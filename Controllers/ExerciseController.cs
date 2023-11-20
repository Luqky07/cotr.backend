﻿using cotr.backend.Model;
using cotr.backend.Model.Request;
using cotr.backend.Model.Response;
using cotr.backend.Service.Exercise;
using cotr.backend.Service.Header;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cotr.backend.Controllers
{
    [ApiController]
    [Route("exercise")]
    [Authorize(AuthenticationSchemes = "Access")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        private readonly IHeaderService _headerService;

        public ExerciseController(IExerciseService exerciseService, IHeaderService headerService)
        {
            _exerciseService = exerciseService;
            _headerService = headerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestAsync(string? statement, string? author)
        {
            try
            {
                ExercisesResponse res = await _exerciseService.GetExercises(statement, author);
                return Ok(res);
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewExerciseAsync(CreateExerciseRequest request)
        {
            try
            {
                int userId = _headerService.GetTokenSubUserId(Request.Headers);

                return Ok(await _exerciseService.CreateNewExercise(userId, request));
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }

        [HttpPost("{exerciseId}")]
        public async Task<IActionResult> ExerciseAttemptAsync(long exerciseId, AttemptRequest request)
        {
            try
            {
                int userId = _headerService.GetTokenSubUserId(Request.Headers);

                await _exerciseService.TryExerciseAttemptAsync(userId, exerciseId, request);

                return NoContent();
            }
            catch (ApiException ex)
            {
                return StatusCode(ex.StatusCode, new ApiExceptionResponse(ex));
            }
        }
    }
}