﻿using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.Exercise;
using cotr.backend.Service.Command;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace cotr.backend.Service.Exercise
{
    public class ExerciseService : IExerciseService
    {
        private readonly ICommandService _commandService;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IConfiguration _config;
        private readonly static string classPattern = @"public\s+class\s+(\w+)\s*{";
        public ExerciseService(ICommandService commandService, IExerciseRepository exerciseRepository, IConfiguration config)
        {
            _commandService = commandService;
            _exerciseRepository = exerciseRepository;
            _config = config;
        }

        public async Task<ExercisesResponse> GetExercises(string? statement, string? author)
        {
            List<ExerciseData> exercises = await _exerciseRepository.GetExercisesAsync();

            if (statement != null) exercises = exercises.Where(x => x.Statement.Contains(statement)).ToList();
            if (author != null) exercises = exercises.Where(x => x.Author.Contains(author)).ToList();

            if ((statement != null || author != null) && exercises.IsNullOrEmpty()) throw new ApiException(404, "No se han encontrado ejercicios con los filtros aplicados");

            return new(exercises);
        }

        public async Task<Exercises> CreateNewExercise(int userId, CreateExerciseRequest request)
        {
            Match match = Regex.Match(request.TestCode, classPattern);

            if (!match.Success) throw new ApiException(409, "El código del test no tiene una clase válida");

            return await _exerciseRepository.SaveExerciseAsync(new(userId, request.LanguajeId, request.Statement, false, true, DateTime.UtcNow, request.TestCode, match.Groups[1].Value));
        }

        public async Task TryExerciseAttemptAsync(int userId, long exerciseId, AttemptRequest request)
        {
            Exercises exerciseInfo = await _exerciseRepository.GetExerciseByIdAsync(exerciseId);

            Dictionary<short, string> languajesDefault = new()
            {
                {1, "java_default" }
            };

            if (!languajesDefault.TryGetValue(exerciseInfo.LanguajeId, out string? defaultDirectory) || defaultDirectory is null) throw new ApiException(500, "No se ha encontrado la ruta por defecto para ese lenguaje de programación");

            string defaultRoute = _config.GetValue<string>("ExercisesRoute") ?? throw new ApiException(500, "No se ha podido cargar la configuración de la ruta de ejercicios");

            string userExerciseRoute = $"{defaultRoute}/{userId}_{exerciseId}";

            if (Directory.Exists(userExerciseRoute)) Directory.Delete(userExerciseRoute, true);

            Directory.CreateDirectory(userExerciseRoute);

            try
            {
                CopyDirectory($"{defaultRoute}/{defaultDirectory}", userExerciseRoute);
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }

            Dictionary<short, Func<Task<CommandRun>>> functions = new()
            {
                {1, async () => await ExecuteJavaExerciseAsync(userExerciseRoute, request.Code, exerciseInfo.TestClassName, exerciseInfo.TestCode)}
            };

            CommandRun exec = await functions[exerciseInfo.LanguajeId].Invoke();

            if (!exec.Error.IsNullOrEmpty()) 
            {
                await _exerciseRepository.SaveAttemptAsync(new(userId, exerciseId, DateTime.UtcNow, false, request.Code, exec.Error));
                throw new ApiException(400, $"Error en la ejecucuión del test:\n{exec.Error}");
            };
            if (exec.Result.Contains("FAILURES!!!"))
            {
                await _exerciseRepository.SaveAttemptAsync(new(userId, exerciseId, DateTime.UtcNow, false, request.Code, exec.Result));
                throw new ApiException(400, $"Test Fail:\n{exec.Result}");
            }

            await _exerciseRepository.SaveAttemptAsync(new(userId, exerciseId, DateTime.UtcNow, true, request.Code, exec.Result));
        }

        private async Task<CommandRun> ExecuteJavaExerciseAsync(string exerciseRoute, string code, string testClassName, string testCode)
        {
            Match match = Regex.Match(code, classPattern);
            if (!match.Success) throw new ApiException(409, "El código proporcionado no tiene una clase válida");

            string codePath = $"{exerciseRoute}/src/code/{match.Groups[1].Value}.java";
            string testPath = $"{exerciseRoute}/src/test/{testClassName}.java";

            try
            {
                File.Create(codePath).Close();
                await File.WriteAllTextAsync(codePath, code);

                File.Create(testPath).Close();
                await File.WriteAllTextAsync(testPath, testCode);
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }

            CommandRun compile = _commandService.ExecuteCommand("javac", $"-cp \".:lib/*:src\" -d bin src/test/{testClassName}.java", exerciseRoute);

            if (!compile.Error.IsNullOrEmpty()) throw new ApiException(400, $"Error en la compilación del test, el ejercicio está mal creado:\n{compile.Error}");

            return _commandService.ExecuteCommand("java", $"-cp \".:lib/*:bin\" org.junit.runner.JUnitCore test.{testClassName}", exerciseRoute);
        }

        private static void CopyDirectory(string sourceDir, string destDir)
        {
            string[] dirs = Directory.GetDirectories(sourceDir);

            foreach (string dir in dirs)
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                Directory.CreateDirectory($"{destDir}/{dir.Split("/")[^1]}");
                CopyDirectory(dir, destSubDir);
            }
            string[] files = Directory.GetFiles(sourceDir);

            foreach (string file in files)
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }
        }
    }
}