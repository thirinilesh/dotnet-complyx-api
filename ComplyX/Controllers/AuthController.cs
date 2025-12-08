using ComplyX.Models;
using ComplyX.Services;
using FluentValidation.Results;
using Lakshmi.Aca.Api.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lakshmi.Api.Controller
{

    /// <summary>
    ///     Operations related to API Authentication
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("/authenticate")]
    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthenticationService _authenticationsService;
        private readonly IUserService _IUserService;
        public AuthController(ILogger<AuthController> logger, IAuthenticationService authenticationsService, IUserService IUserService)
        {
            _logger = logger;
            _authenticationsService = authenticationsService;
            _IUserService = IUserService;
        }

        /// <summary>
        ///     Get auth token
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequestModel request)
        {
            var validator = new AuthRequestModelValidator();
            ValidationResult validationResult = validator.Validate(request);
            ErrorResponse errorResponse = HandleValidationErrors(validationResult);
            if (errorResponse != null)
            {
                _logger.LogError("Authentication failed - validation errors: {Errors}", errorResponse.Error);
                return BadRequest(errorResponse);
            }

            var response = await _IUserService.Authenticate(request);
            return Ok(response);
        }

        private ErrorResponse HandleValidationErrors(ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }
    }
} 