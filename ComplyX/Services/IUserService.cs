using ComplyX.Helper;
using ComplyX.Models;
using ComplyX.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Lakshmi.Aca.Api.Controllers;

namespace ComplyX.Services
{
    public interface IUserService 

    {
       
        Task<ManagerBaseResponse<RegisterUser>> Register(RegisterUser RegisterUser);
        Task<ManagerBaseResponse<Login>> Login(Login Login);
        Task<AuthRequestModel> Authenticate(AuthRequestModel request);
        Task<ManagerBaseResponse<ForgotPasswordVerifyModel>> ForgotPassword([FromBody] ForgotPasswordVerifyModel request);
        Task<ManagerBaseResponse<ResetPasswordRequestModel>> ResetPassword([FromBody] ResetPasswordRequestModel request);
        Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model);
    }
}
