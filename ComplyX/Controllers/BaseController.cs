 
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using ComplyX.Shared.Helper;
using ComplyX_Businesss.Models;



namespace Lakshmi.Aca.Api.Controllers
{
    /// <summary>
    /// Base controller for all API controllers in the application.
    /// Provides common helper methods and standard response handling for derived controllers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Converts a <see cref="ManagerBaseResponse{T}"/> into an <see cref="IActionResult"/> for API responses.
        /// </summary>
        /// <typeparam name="T">The type of the data contained in the response.</typeparam>
        /// <param name="response">The manager response containing data and status information.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the outcome:
        /// - Returns 200 OK with the response data if the operation was successful.
        /// - Returns 400 Bad Request if the operation failed.
        /// </returns>
        public IActionResult ResponseResult<T>(ManagerBaseResponse<T> response)
        {
            if (response.IsSuccess)
            {
                if (response.PageDetail == null)
                {
                    return Ok(new ApiBaseResponse<T>()
                    {

                        Message = response.Message,
                        Result = response.Result,
                        IsSuccess = true,
                        StatusCode = response.StatusCode
                    });
                }

                return Ok(new ApiBasePageResponse<T>()
                {

                    Message = response.Message,
                    Result = response.Result,
                    PageDetail = response.PageDetail,
                    IsSuccess = true,
                    StatusCode = response.StatusCode
                });
            }
            else if (response.StatusCode == 401)
            {
                return Unauthorized(new ApiBaseFailResponse<T>()
                {

                    Message = response.Message,
                    Result = response.Result,
                    IsSuccess = false,
                    StatusCode = response.StatusCode
                });
            }
            else
            {
                return BadRequest(new ApiBaseFailResponse<T>()
                {

                    Message = response.Message,
                    Result = response.Result,
                    IsSuccess = false,
                    StatusCode = response.StatusCode
                });
            }
        }
        /// <summary>
        /// Handles validation errors returned by a <see cref="ValidationResult"/>.
        /// Converts the validation errors into an <see cref="IActionResult"/> to return to the client.
        /// </summary>
        /// <param name="validationResult">The validation result containing any errors to handle.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the outcome of the validation:
        /// - Returns 400 Bad Request with error details if validation fails.
        /// - Returns 200 OK if no validation errors are present.
        /// </returns>
        protected ErrorResponse HandleValidationErrors(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                ErrorResponse response = new ErrorResponse
                {
                    Error = validationResult.Errors.Select(e => e.ErrorMessage)
                        .Aggregate((current, next) => $"{current}, {next}"),
                    Code = StatusCodes.Status400BadRequest
                };
                return response;
            }
            return null;
        }
    }
}
