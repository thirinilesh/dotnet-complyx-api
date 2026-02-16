using ComplyX_Businesss.Models;
using ComplyX.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ComplyX.Shared    .Helper;
using Microsoft.AspNetCore.Identity;
using ComplyX.Controllers;
using Microsoft.AspNetCore.Mvc;
using Lakshmi.Aca.Api.Controllers;
using ComplyX.Shared.Data;
using ComplyX.Data.Entities;
using Azure.Core;
using ComplyX.Data.Entities;

namespace ComplyX.Controllers
{
    /// <summary>
    /// Controller for managing user operations.
    /// Provides endpoints to create, update, retrieve, and manage user accounts.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserController : BaseController
    {
        private readonly IUserService _IUserService;
        private readonly UserManager<ApplicationUsers> _userManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="IUserService">The service used to manage user operations.</param>
        /// <param name="userManager">The user manager for handling application users.</param>
        public UserController(IUserService IUserService, UserManager<ApplicationUsers> userManager)
        {
            _IUserService = IUserService;
            _userManager = userManager;
        }
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="RegisterUser">
        /// The details of the user to be registered, including name, email, password, and any other required fields.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the registration operation.
        /// </returns>
        /// <response code="200">The user was registered successfully.</response>
        /// <response code="400">If there is an error during registration, such as validation failures or duplicate email.</response>

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Data.Entities.RegisterUser RegisterUser)
        {
            return ResponseResult(await _IUserService.Register(RegisterUser));
        }
        /// <summary>
        /// Authenticates a user and returns an access token if the credentials are valid.
        /// </summary>
        /// <param name="dto">
        /// The login details, including username/email and password.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing authentication result and token if successful.
        /// </returns>
        /// <response code="200">The user was authenticated successfully and a token is returned.</response>
        /// <response code="400">If the credentials are invalid or there is an error during authentication.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]ComplyX_Businesss.Models.Logins.LoginRequestModel dto)
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
        /// <summary>
        /// Changes the password for a user.
        /// </summary>
        /// <param name="model">
        /// The details required to change the password, including current password and new password.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the password change operation.
        /// </returns>
        /// <response code="200">The password was changed successfully.</response>
        /// <response code="400">If there is an error while changing the password, such as invalid current password or validation failure.</response>

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
        /// <summary>
        /// Assigns one or more roles to a user.
        /// </summary>
        /// <param name="request">
        /// The details of the user and the roles to assign, including the UserId and list of RoleIds.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> indicating the result of the role assignment operation.
        /// </returns>
        /// <response code="200">Roles were assigned to the user successfully.</response>
        /// <response code="400">If there is an error during role assignment, such as invalid user or role IDs.</response>
        /// <response code="401">If the request is unauthorized.</response>
        [Authorize]
        [HttpPost("AssignRolesToUser")]         
        public async Task<IActionResult> AssignRolesToUser([FromBody] AssignRoleToUser request)
        {
            return ResponseResult(await _IUserService.AssignRoleToUser(request));
        }

        /// <summary>
        /// Retrieves the list of users.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of users.
        /// If no users are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of users.</response>
        /// <response code="204">If no users are found.</response>
        /// <response code="400">If there is an error while fetching the users.</response>
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IUserService.GetUserList(PagedListCriteria));
        }

        /// <summary>
        /// Retrieves the list of roles.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the list of roles.
        /// If no roles are found, it returns a 204 No Content response.
        /// If an error occurs during the retrieval process, it returns a 400 Bad Request response.
        /// </returns>
        /// <response code="200">Returns the list of roles.</response>
        /// <response code="204">If no roles are found.</response>
        /// <response code="400">If there is an error while fetching the roles.</response>
        [HttpGet("GetRoleList")]
        public async Task<IActionResult> GetRoleList([FromQuery] PagedListCriteria PagedListCriteria)
        {
            return ResponseResult(await _IUserService.GetRoleList(PagedListCriteria));
        }

    }
}
