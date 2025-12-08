using ComplyX.Helper;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using ComplyX.Helper;
using ComplyX.Models;



namespace Lakshmi.Aca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
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
