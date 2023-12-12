using cotr.backend.Model;
using cotr.backend.Model.DataModel;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using cotr.backend.Repository.Exercise;
using cotr.backend.Service.Command;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace cotr.backend.Service.Exercise
{
    public partial class ExerciseService : IExerciseService
    {
        private readonly ICommandService _commandService;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IConfiguration _config;
        private readonly static string classPattern = @"public\s+class\s+(\w+)\s*{";
        private readonly static string javascriptFail = @"\b([1-9]\d*)\s+failing\b";
        public ExerciseService(ICommandService commandService, IExerciseRepository exerciseRepository, IConfiguration config)
        {
            _commandService = commandService;
            _exerciseRepository = exerciseRepository;
            _config = config;
        }

        public async Task<ExercisesResponse> GetExercisesAsync(int userId, string? statement, string? author, short? languageId, int? creatorId)
        {
            List<ExerciseDataResponse> exercises = await _exerciseRepository.GetExercisesAsync();
            exercises = exercises.Where(x => x.Exercise.IsAproved || x.Author.UserId.Equals(userId)).ToList();

            if (statement != null) exercises = exercises.Where(x => x.Exercise.Statement.Contains(statement)).ToList();
            if (author != null) exercises = exercises.Where(x => x.Author.NickName.Contains(author)).ToList();
            if (languageId != null) exercises = exercises.Where(x => x.Language.LanguageId.Equals(languageId)).ToList();
            if (creatorId != null) exercises = exercises.Where(x => x.Author.UserId.Equals(creatorId)).ToList();

            return new(exercises);
        }

        public async Task<Exercises> CreateNewExercise(int userId, CreateExerciseRequest request)
        {
            string className = "test";
            if(request.LanguageId == 1)
            {
                Match match = Regex.Match(request.TestCode, classPattern);

                if (!match.Success) throw new ApiException(409, "El código del test no tiene una clase válida");
                className = match.Groups[1].Value;
            }

            return await _exerciseRepository.SaveExerciseAsync(new(userId, request.LanguageId, request.Statement, false, true, DateTime.UtcNow, request.TestCode, className));
        }

        public async Task TryExerciseAttemptAsync(int userId, long exerciseId, AttemptRequest request)
        {
            Exercises exerciseInfo = await _exerciseRepository.GetExerciseByIdAsync(exerciseId);

            if (!exerciseInfo.IsAproved && !exerciseInfo.CreatorId.Equals(userId)) throw new ApiException(400, "No puedes intentar resolver un ejercicio que no esté validado");

            string defaultDirectory;

            if (exerciseInfo.LanguageId == 1) defaultDirectory = "java_default";
            else if (exerciseInfo.LanguageId == 2) defaultDirectory = "javascript_default";
            else throw new ApiException(500, "No se ha encontrado la ruta por defecto para ese lenguaje de programación");

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

            CommandRun exec;
            if (exerciseInfo.LanguageId == 1) exec = await ExecuteJavaExerciseAsync(userExerciseRoute, request.Code, exerciseInfo.TestClassName, exerciseInfo.TestCode);
            else if (exerciseInfo.LanguageId == 2) exec = await ExecuteJavaScriptExerciseAsync(userExerciseRoute, request.Code, exerciseInfo.TestCode);
            else throw new ApiException(500, "No se ha encontrado la función para ejecutar ese lenguaje de programación");

            if (!exec.Error.IsNullOrEmpty()) 
            {
                await _exerciseRepository.SaveAttemptAsync(new(userId, exerciseId, DateTime.UtcNow, false, request.Code, exec.Error));
                throw new TryException("Fallo en el test", exec.Error);
            };
            if (exec.Result.Contains("FAILURES!!!") || Regex.IsMatch(exec.Result, javascriptFail))
            {
                await _exerciseRepository.SaveAttemptAsync(new(userId, exerciseId, DateTime.UtcNow, false, request.Code, exec.Result));
                throw new TryException("Fallo en el ejercicio", exec.Result);
            }

            await _exerciseRepository.SaveAttemptAsync(new(userId, exerciseId, DateTime.UtcNow, true, request.Code, exec.Result));
            if (!exerciseInfo.IsAproved && exerciseInfo.CreatorId.Equals(userId))
            {
                exerciseInfo.IsAproved = true;
                await _exerciseRepository.UpdateExerciseAsync(exerciseInfo);
            }
        }

        public async Task<ExerciseDataResponse> GetExerciseInfoByIdAsync(int userId, long exerciseId)
        {
            ExerciseDataResponse exercise = await _exerciseRepository.GetExerciseInfoByIdAsync(exerciseId);

            if (!exercise.Exercise.IsAproved && !exercise.Author.UserId.Equals(userId)) throw new ApiException(409, "No se puede obtener información del ejercicio si el autor no lo ha resuelto");

            return exercise;
        }

        public async Task<TestDataResponse> GetExerciseTestInfoByIdAsync(int userId, long exerciseId)
        {
            TestDataResponse exercise = await _exerciseRepository.GetExerciseTestInfoByIdAsync(exerciseId);

            if (!exercise.Author.UserId.Equals(userId)) throw new ApiException(409, "No se puede obtener información del test de un ejercicio creado por otro usuario");

            return exercise;
        }

        public async Task EditExerciseTestAsync(int userId, long exerciseId, EditExerciseRequest request)
        {
            Exercises exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseId);

            if (!exercise.CreatorId.Equals(userId)) throw new ApiException(409, "No se puede editar un ejercicio si no eres su autor");

            if (exercise.TestCode.Equals(request.TestCode) && exercise.Statement.Equals(request.Statement)) throw new ApiException(409, "Para editar un ejercicio el código o el enunciado debe ser diferente del que ya estaba guardado");

            exercise.TestCode = request.TestCode;
            exercise.Statement = request.Statement;

            await _exerciseRepository.UpdateExerciseAsync(exercise);
        }

        #region Funciones Privadas
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

            if (!compile.Error.IsNullOrEmpty()) throw new ApiException(400, $"Error en la compilación del test:\n{compile.Error}");

            return _commandService.ExecuteCommand("java", $"-cp \".:lib/*:bin\" org.junit.runner.JUnitCore test.{testClassName}", exerciseRoute);
        }

        private async Task<CommandRun> ExecuteJavaScriptExerciseAsync(string exerciseRoute, string code, string testCode)
        {

            string codePath = $"{exerciseRoute}/test.js";

            _commandService.ExecuteCommand("npm", "init -y", exerciseRoute);
            _commandService.ExecuteCommand("npm", "install mocha chai sinon --save-dev", exerciseRoute);

            string packageDocument = $"{exerciseRoute}/package.json";
            string packageContent = await File.ReadAllTextAsync(packageDocument);

            packageContent = packageContent.Replace("echo \\\"Error: no test specified\\\" && exit 1", "mocha");

            await File.WriteAllTextAsync(packageDocument, packageContent);

            try
            {
                File.Create(codePath).Close();
                await File.WriteAllTextAsync(codePath, $"{code}\n\n{testCode}");
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }

            return _commandService.ExecuteCommand("npm", "test", exerciseRoute);
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
        #endregion
    }
}