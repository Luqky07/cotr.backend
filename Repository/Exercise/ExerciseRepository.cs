using cotr.backend.Data;
using cotr.backend.Model;
using cotr.backend.Model.Response;
using cotr.backend.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace cotr.backend.Repository.Exercise
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly CotrContext _context;

        public ExerciseRepository(CotrContext context)
        {
            _context = context;
        }

        public async Task<List<ExerciseDataResponse>> GetExercisesAsync()
        {
            try
            {
                return await (
                    from exercise in _context.Exercises
                    join users in _context.Users
                        on exercise.CreatorId equals users.UserId
                    join languages in _context.Languages
                        on exercise.LanguageId equals languages.LanguageId
                    select new ExerciseDataResponse(
                        new(users.UserId, users.Nickname),
                        new(exercise.ExerciseId, exercise.Statement, exercise.IsAproved, exercise.CreationDate),
                        new(languages.LanguageId, languages.Name, languages.CodeStart)
                    )
                ).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<Exercises> SaveExerciseAsync(Exercises exercise)
        {
            try
            {
                var res = await _context.Exercises.AddAsync(exercise);
                await _context.SaveChangesAsync();

                return res.Entity;
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<Exercises> GetExerciseByIdAsync(long exerciseId)
        {
            try
            {
                return await _context.Exercises.FirstOrDefaultAsync(x => x.ExerciseId.Equals(exerciseId)) ?? throw new ApiException(404, "No se ha encontrado información del ejercicio");
            }
            catch(Exception ex )
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<UserExerciseAttempts> SaveAttemptAsync(UserExerciseAttempts attempt)
        {
            try
            {
                var res = await _context.UserExerciseAttempts.AddAsync(attempt);
                await _context.SaveChangesAsync();

                return res.Entity;
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task UpdateExerciseAsync(Exercises exercise)
        {
            try
            {
                _context.Exercises.Update(exercise);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<ExerciseDataResponse> GetExerciseInfoByIdAsync(long exerciseId)
        {
            try
            {
                return await (
                    from exercise in _context.Exercises
                    join users in _context.Users
                        on exercise.CreatorId equals users.UserId
                    join languages in _context.Languages
                        on exercise.LanguageId equals languages.LanguageId
                    where exercise.ExerciseId.Equals(exerciseId)
                    select new ExerciseDataResponse(
                        new(users.UserId, users.Nickname),
                        new(exercise.ExerciseId, exercise.Statement, exercise.IsAproved, exercise.CreationDate),
                        new(languages.LanguageId, languages.Name, languages.CodeStart)
                    )
                ).FirstOrDefaultAsync() ?? throw new ApiException(404, "Ejercicio no encontrado");
            }
            catch(Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }

        public async Task<TestDataResponse> GetExerciseTestInfoByIdAsync(long exerciseId)
        {
            try
            {
                return await (
                    from exercise in _context.Exercises
                    join users in _context.Users
                        on exercise.CreatorId equals users.UserId
                    join languages in _context.Languages
                        on exercise.LanguageId equals languages.LanguageId
                    where exercise.ExerciseId.Equals(exerciseId)
                    select new TestDataResponse(
                        new(users.UserId, users.Nickname),
                        new(exercise.ExerciseId, exercise.Statement, exercise.TestCode, exercise.IsAproved),
                        new(languages.LanguageId, languages.Name, languages.CodeStart)
                    )
                ).FirstOrDefaultAsync() ?? throw new ApiException(404, "Ejercicio no encontrado");
            }
            catch (Exception ex)
            {
                if (ex is ApiException apiEx) throw apiEx;
                throw new ApiException(500, ex.Message);
            }
        }
    }
}
