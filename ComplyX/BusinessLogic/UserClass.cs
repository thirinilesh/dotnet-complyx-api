using ComplyX.Helper;
using ComplyX.Models;
using Microsoft.AspNetCore.Identity;
using ComplyX.Data.Identity;
using ComplyX.Data;
using System.Security.Claims;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Lakshmi.Aca.Api.Controllers;
using ComplyX.Services;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Azure;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;
using Jose.native;
using System.Web.Providers.Entities;

namespace ComplyX.BusinessLogic
{
    public class UserClass : IUserService
    {
        private readonly IUserService _IUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenService _tokenService;
        private readonly AppDbContext _context;
 

        public UserClass( AppDbContext context, UserManager<ApplicationUser> userManager, JwtTokenService tokenservice)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenservice;
 
        }

        public async Task<ManagerBaseResponse<RegisterUser>> Register(RegisterUser RegisterUser)
        {
            try
            {
                if (string.IsNullOrEmpty(RegisterUser.Email))
                {
                    return (new ManagerBaseResponse<RegisterUser>()
                    {
                        Result = null,
                        Message = "Requested model is not valid.",
                        StatusCode = 500
                    });
                }
                else
                {
                    var findUser = await _userManager.Users.Where(x => x.UserName.ToLower().Trim() == RegisterUser.UserName.ToLower().Trim()).FirstOrDefaultAsync();
                    var UserName = await _userManager.FindByNameAsync(RegisterUser.Email);
                    if (UserName != null)
                    {
                        return (new ManagerBaseResponse<RegisterUser>() { Message = "UserName already registered" });
                    }
                    if (UserName != null)
                    {
                        var message = string.Empty;
                        if (UserName.UserName.ToLower().Trim() == RegisterUser.UserName.ToLower().Trim())
                        {
                            message = $"UserName '{RegisterUser.UserName}' is already taken.";
                        }

                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            IsSuccess = false,
                            Result = null,
                            Message = message
                        });
                    }

                    bool hasUpper = false;
                    bool hasLower = false;
                    bool hasDigit = false;
                    bool hasSpecial = false;
                    bool repeats = false;
                    char lastChar = ' ';
                    string special = "!@#$%^&*()-+=_~`<>,.?/|\\ ";
                    //   var isUserInCreateUserRole = await _userManager.IsInRoleAsync(loggedInUser, "AddUsersNonAdmin");

                    // check for 1 of each, upper, lower, number, special
                    foreach (char c in RegisterUser.Password)
                    {
                        if (char.IsUpper(c)) { hasUpper = true; }
                        else if (char.IsLower(c)) { hasLower = true; }
                        else if (char.IsDigit(c)) { hasDigit = true; }
                        if (lastChar.Equals(c)) { repeats = true; }
                        if (special.Contains(c.ToString())) { hasSpecial = true; }
                        lastChar = c;
                    }

                    if (RegisterUser.UserName.Length < 8)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "User Name must be at least 8 characters.",
                            StatusCode = 500
                        });
                    }

                    if (RegisterUser.Password.Length < 8)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "Password must be at least 8 characters.",
                            StatusCode = 500
                        });
                    }

                    if (!hasUpper)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "Password must contain at least one uppercase letter.",
                            StatusCode = 500
                        });
                    }

                    if (!hasLower)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "Password must contain at least one lowercase letter.",
                            StatusCode = 500
                        });
                    }
                    if (!hasDigit)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "Password must contain at least one number.",
                            StatusCode = 500
                        });
                    }

                    if (!hasSpecial)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "Password must contain at least one special character.",
                            StatusCode = 500
                        });
                    }

                    if (RegisterUser.Password == RegisterUser.UserName)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "Password cannot match the user name.",
                            StatusCode = 500
                        });
                    }

                    if (RegisterUser.Password == RegisterUser.Email)
                    {
                        return (new ManagerBaseResponse<RegisterUser>()
                        {
                            Result = null,
                            Message = "Password cannot be the same as the email address.",
                            StatusCode = 500
                        });
                    }

                    var user = new ApplicationUser()
                    {
                        UserName = RegisterUser.UserName,
                        Email = RegisterUser.Email,
                        EmailConfirmed = true,
                        PhoneNumber = RegisterUser.Phone,
                        IsApproved = true,
                        ApprovedDeniedBy = "",
                        ApprovedDeniedDate = DateTime.UtcNow,
                    };
                    IdentityResult result = await _userManager.CreateAsync(user, RegisterUser.Password);
                    RegisterUser _model = new RegisterUser();
                    if (_model != null)

                    {
                        //_model.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(RegisterUser.Password));  
                        string hashed = BCrypt.Net.BCrypt.HashPassword(RegisterUser.Password);
                       
                        _model.UserName = RegisterUser.UserName;
                        _model.Password = hashed;
                        _model.Domain = RegisterUser.Domain;
                        _model.Email = RegisterUser.Email;
                        _model.Phone = RegisterUser.Phone;
                        _model.Address = RegisterUser.Address;
                        _model.State = RegisterUser.State;
                        _model.GSTIN = RegisterUser.GSTIN;
                        _model.PAN = RegisterUser.PAN;
                      

                        _context.Add(_model);
                        _context.SaveChanges();

                    }
                    var response = new RegisterUser();
                     
                    return (new ManagerBaseResponse<RegisterUser>()
                    {
                        IsSuccess = true,
                        Result = response,
                        Message = "User account created successfully.",
                    });
                }
            }
            catch(Exception ex)
            {

                return (new ManagerBaseResponse<RegisterUser>()
                {
                    IsSuccess = true,
                    Result = null,
                    Message = ex.Message,
                });
            }
        }

        public async Task<ManagerBaseResponse<Login>> Login(Login Login)
        {
           // var inputpass = Convert.ToBase64String(Encoding.UTF8.GetBytes(Login.Password));
            // Replace with real authentication logic
            var user = await _context.RegisterUser
                    .FirstOrDefaultAsync(x => x.UserName == Login.Username);
            bool inputpass = BCrypt.Net.BCrypt.Verify(Login.Password, user.Password);

            if (user != null && inputpass == true)
            {
                // Login success
                var token = _tokenService.GenerateToken(Login.Username);
                var response = new Login
                {

                    token = token
                };

                return (new ManagerBaseResponse<Login>()
                {
                    Result =  response,
                    Message = "Username and Password is vaild.",
                    StatusCode = 200
                });
            }
            else
            {
                // Invalid username/password
                return (new ManagerBaseResponse<Login>()
                {
                    Result = null,
                    IsSuccess= false,
                    Message = "Invalid username/password",
                    StatusCode = 401
                });
            }

        }

        public async Task<AuthRequestModel> Authenticate(AuthRequestModel request)
        {
            var jwt = request.Assertion;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(jwt);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "iss")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new AuthRequestModel
                {
                    Message = "Invalid JWT - User ID not found in token.",
                };
               
            }

            //this._logger.LogInformation($"Looking up auth config for User ID from JWT: {userId}");
           
            //this._logger.LogInformation($"Found partner API auth config for User ID: {userId}");
            // verify jwt signature with public key
            var rsa = RSA.Create();
            
            try
            {
                tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(rsa),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

            }
               catch (Exception ex)
            {
                return new AuthRequestModel
                {
                     
                    Message = ex.Message,
                    Token = null,
                    
                };
            }

        // if valid, generate a new short-lived JWT for the user
        var newJwt = _tokenService.GenerateToken(userId);

            return new AuthRequestModel { Assertion = newJwt, GrantType = "urn:ietf:params:oauth:grant-type:jwt-bearer" };
        }

        public async Task<ManagerBaseResponse<ForgotPasswordVerifyModel>> ForgotPassword([FromBody] ForgotPasswordVerifyModel request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Username))
                {
                    return (new ManagerBaseResponse<ForgotPasswordVerifyModel>()
                    {
                        Result = null,
                        IsSuccess = false,
                        Message = "Username is required.",
                        StatusCode = 401
                         
                    });
                }
                // Try find user (username can be UserName or Email)
                var user = await _userManager.FindByNameAsync(request.Username)
                           ?? await _userManager.FindByEmailAsync(request.Username);

                if (user == null)
                {
                    return (new ManagerBaseResponse<ForgotPasswordVerifyModel>()
                    {
                        Result = null,
                        IsSuccess = false,
                        Message = "User not found.",
                        StatusCode = 401

                    });
                }
                // Generate reset token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var response = new ForgotPasswordVerifyModel
                {
                    Username = request.Username,
                    Token = token,
                };

                return new ManagerBaseResponse<ForgotPasswordVerifyModel>()
                {
                    Result = response,
                    IsSuccess = false,
                    Message = "Password reset token generated successfully.",
                };

            }
            catch (Exception ex)
            {
                return (new ManagerBaseResponse<ForgotPasswordVerifyModel>()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message,                     
                });
            }            
        }

        public async Task<ManagerBaseResponse<ResetPasswordRequestModel>> ResetPassword([FromBody] ResetPasswordRequestModel request)
        {              
            if (request == null || string.IsNullOrWhiteSpace(request.Username))
            {
                
                //return Ok(response);
                return (new ManagerBaseResponse<ResetPasswordRequestModel>()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Username is required.",
                    StatusCode = 401

                });
            }

            try
            {
                // Find user by username or email
                var user = await _userManager.FindByNameAsync(request.Username)
                           ?? await _userManager.FindByEmailAsync(request.Username);

                if (user == null)
                {
                    return (new ManagerBaseResponse<ResetPasswordRequestModel>()
                    {
                        Result = null,
                        IsSuccess = false,
                        Message = "User not found.",
                        StatusCode = 401

                    });
                }
                var response = new ManagerBaseResponse<ResetPasswordRequestModel>();
                // Reset the password using the token
                var resetResult = await _userManager.ResetPasswordAsync(user, request.token, request.Password);

                if (!resetResult.Succeeded)
                {
                    return (new ManagerBaseResponse<ResetPasswordRequestModel>
                    {
                        IsSuccess = false,
                        Message = string.Join(", ", resetResult.Errors.Select(e => e.Description)),
                        Result = response.Result
                    });
                }

                // Update LastPasswordChangeDate
                user.LastPasswordChangeDate = DateTime.UtcNow;
                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    return  (new ManagerBaseResponse<ResetPasswordRequestModel>
                    {
                        IsSuccess = false,
                        Message = string.Join(", ", updateResult.Errors.Select(e => e.Description)),
                        Result = response.Result
                    });
                }

                // Success response

                return new ManagerBaseResponse<ResetPasswordRequestModel>()
                {
                    Result = response.Result,
                    IsSuccess = false,
                    Message = "Password reset successfully.",
                };
            }
            catch (Exception ex)
            {
                 return (new ManagerBaseResponse<ResetPasswordRequestModel>()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message,                     
                });
            }
        }

      

    }
}
