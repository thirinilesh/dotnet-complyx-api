using ComplyX.Shared.Helper;
using ComplyX.Data.Entities;
using ComplyX.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using ComplyX_Businesss.Models;
using RegisterUser = ComplyX.Data.Entities.RegisterUser;
using ComplyX_Businesss.Models.Logins;

namespace ComplyX.Services
{
    public interface IUserService 

    {
       
        Task<ManagerBaseResponse<RegisterUser>> Register(RegisterUser RegisterUser);
        Task<ManagerBaseResponse<LoginResponseModel>> Login(LoginRequestModel Login);
        Task<AuthRequestModel> Authenticate(AuthRequestModel request);
        Task<ManagerBaseResponse<ForgotPasswordVerifyModel>> ForgotPassword([FromBody] ForgotPasswordVerifyModel request);
        Task<ManagerBaseResponse<ResetPasswordRequestModel>> ResetPassword([FromBody] ResetPasswordRequestModel request);
        Task<ManagerBaseResponse<ChangePasswordModel>> ChangePassword([FromBody] ChangePasswordModel model);
        Task<ManagerBaseResponse<bool>> CreateRoleAsync(string  rolename);
        Task<ManagerBaseResponse<bool>> AssignRoleToUser(AssignRoleToUser request);
        Task<ManagerBaseResponse<List<RegisterUser>>> GetUserList();
        Task<ManagerBaseResponse<List<AspNetRole>>> GetRoleList();
    }
}
