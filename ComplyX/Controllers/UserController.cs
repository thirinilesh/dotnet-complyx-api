using ComplyX.Models;
using ComplyX.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ComplyX.Helper;
using Microsoft.AspNetCore.Identity;
using ComplyX.Controllers;
using Microsoft.AspNetCore.Mvc;
using Lakshmi.Aca.Api.Controllers;
using ComplyX.Data;
using Azure.Core;

namespace ComplyX.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserController : BaseController
    {
        private readonly IUserService _IUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserService IUserService, UserManager<ApplicationUser> userManager)
        {
            _IUserService = IUserService;
            _userManager = userManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser RegisterUser)
        {
            return ResponseResult(await _IUserService.Register(RegisterUser));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login dto)
        {
            return ResponseResult(await _IUserService.Login(dto));
        }

        /// <summary>
        /// Forgot password using email/user name.
        /// Generates a password reset token for the user.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        [ProducesResponseType(typeof(ApiBaseFailResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordVerifyModel request)
        {
            return ResponseResult(await _IUserService.ForgotPassword(request));
        }

        /// <summary>
        /// Reset a user's password using a reset token.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        [ProducesResponseType(typeof(ApiBaseFailResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestModel request)
        {
            return ResponseResult(await _IUserService.ResetPassword(request));
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            return ResponseResult(await _IUserService.ChangePassword(model));
        }

        /// <summary>
        /// Create a new role.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        /// <returns>An IActionResult containing the result of the role creation.</returns>
        [Authorize]
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            return ResponseResult(await _IUserService.CreateRoleAsync(roleName));
        }

        [Authorize]
        [HttpPost("AssignRolesToUser")]         
        public async Task<IActionResult> AssignRolesToUser([FromBody] AssignRoleToUser request)
        {
            return ResponseResult(await _IUserService.AssignRoleToUser(request));
        }
    }
}
