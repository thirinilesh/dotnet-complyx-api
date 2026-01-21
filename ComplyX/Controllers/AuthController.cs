using ComplyX_Businesss.Models;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging information and errors.</param>
        /// <param name="authenticationsService">The service used for handling authentication operations.</param>
        /// <param name="IUserService">The service used for managing user-related operations.</param>
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

        private new ErrorResponse HandleValidationErrors(ValidationResult validationResult)
        {
            throw new NotImplementedException();
        }
    }
} 