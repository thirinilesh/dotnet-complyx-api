using ComplyX.Services;
using ComplyX_Businesss.Models;
using Microsoft.AspNetCore.Identity;
using ComplyX.Shared.Helper;
using ComplyX;
using System.Web;
using System.Security.Claims;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using ComplyX_Businesss.Services;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Azure;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;
using Jose.native;
 using Azure.Core;
using System.Text.RegularExpressions;
using ComplyX_Businesss.Helper;
using ComplyX.Shared.Data;
using AppContext = ComplyX_Businesss.Helper.AppContext;
using ComplyX.Data.Entities;
using RegisterUser = ComplyX.Data.Entities.RegisterUser;
using ComplyX.Repositories.UnitOfWork;
using ComplyX_Businesss.Models.Logins;


namespace ComplyX.BusinessLogic
{
    public class UserClass : IUserService
    {
 
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly JwtTokenService _tokenService;
        private readonly AppContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _UnitOfWork;

        public UserClass(AppContext context, UserManager<ApplicationUsers> userManager, 
            IUnitOfWork unitOfWork ,JwtTokenService tokenservice, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenservice;
            _roleManager = roleManager;
            _UnitOfWork = unitOfWork;
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

                    var user = new ApplicationUsers()
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
                        _model.Gstin = RegisterUser.Gstin;
                        _model.Pan = RegisterUser.Pan;


                      await  _UnitOfWork.RegisterRespositories.AddAsync(_model);

                    }
                    await _UnitOfWork.CommitAsync();
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

        public async Task<ManagerBaseResponse<LoginResponseModel>> Login(ComplyX_Businesss.Models.Logins.LoginRequestModel Login)
        {
           // var inputpass = Convert.ToBase64String(Encoding.UTF8.GetBytes(Login.Password));
            // Replace with real authentication logic
            var user = await _UnitOfWork.RegisterRespositories.GetQueryable()
                    .FirstOrDefaultAsync(x => x.UserName == Login.Username);
            if (user == null)
            {
                return new ManagerBaseResponse<LoginResponseModel>()
                {
                    Result = null,
                    Message = "Invalid Username or Password.",
                    StatusCode = 401
                };
            }
            bool inputpass = BCrypt.Net.BCrypt.Verify(Login.Password, user.Password);

            if (inputpass == true)
            {
                // Login success
                var token = _tokenService.GenerateToken(Login.Username);
                var response = new LoginResponseModel
                {

                    token = token
                };

                return  new ManagerBaseResponse<LoginResponseModel>()
                {
                    Result =  response,
                    Message = "Username and Password is vaild.",
                    StatusCode = 200
                } ;
            }
            else
            {
                // Invalid username/password
                return new ManagerBaseResponse<LoginResponseModel>()
                {
                    Result = null,
                    IsSuccess= false,
                    Message = "Invalid username/password",
                    StatusCode = 401
                };
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

        public async Task<ManagerBaseResponse<ChangePasswordModel>> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (model == null)
            {     
                return (new ManagerBaseResponse<ChangePasswordModel>()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Invalid model data.",
                    StatusCode = 400

                });
            }


            var user = await _userManager.FindByNameAsync(model.username);

            if (user == null)
            {
                return (new ManagerBaseResponse<ChangePasswordModel>()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "User not found.",
                    StatusCode = 401

                });
            }

            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                 

                return (new ManagerBaseResponse<ChangePasswordModel>()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Please enter valid current password.",
                    StatusCode = 400

                });
            }

            if (model.OldPassword == model.NewPassword)
            {
                return  (new ManagerBaseResponse<ChangePasswordModel>
                {
                    Result = null,
                    Message = "You cannot reuse your old password.",
                    StatusCode = 400
                });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                var message = string.Join(" | ", result.Errors.Select(x => x.Description));
                return (new ManagerBaseResponse<ChangePasswordModel>
                {
                    Result = null,
                    Message = message,
                    StatusCode = 400
                });
            }

            user.LastPasswordChangeDate = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            string hashed = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            var usersname = await _context.RegisterUser.FirstOrDefaultAsync(x => x.UserName == model.username);
             
            usersname.Password = hashed;
            
            _context.Update(usersname);
            _context.SaveChanges();

            var response = new ChangePasswordModel
            {
                NewPassword = model.NewPassword
            };
            return (new ManagerBaseResponse<ChangePasswordModel>
            {
                Result = response,
                Message = "Your password has been changed successfully."
            });
        }

        public async Task<ManagerBaseResponse<bool>> CreateRoleAsync(string roleName)
        {
            // Trim to remove leading/trailing spaces
            roleName = roleName?.Trim();

            // Check if the role name is empty
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = "Role name cannot be empty or whitespace."
                };
            }

            // Validate allowed characters: letters, numbers, underscores, and spaces
            var isValid = Regex.IsMatch(roleName, @"^[a-zA-Z0-9_ ]+$");
            if (!isValid)
            {
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = "Role name can only include letters, numbers, underscores (_) and spaces."
                };
            }

            // Check if the role already exists
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = "Role already exists."
                };
            }

            // Create the role
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = roleName });

            // Return the response
            return new ManagerBaseResponse<bool>
            {
                IsSuccess = result.Succeeded,
                Result = result.Succeeded,
                Message = result.Succeeded
                    ? "Role created successfully."
                    : string.Join("; ", result.Errors.Select(e => e.Description))
            };
        }

        public async Task<ManagerBaseResponse<bool>> AssignRoleToUser(AssignRoleToUser request)
        {
            // Validate request
            if (string.IsNullOrWhiteSpace(request.UserId) ||
                request.RoleName == null ||
                !request.RoleName.Any())
            {
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = "UserId and at least one role name must be provided."
                };
            }
            // Check if user exists
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = "User not found."
                };
            }
            // Validate roles
            var invalidRoles = new List<string>();
            foreach (var roleName in request.RoleName)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    invalidRoles.Add(roleName);
                }
            }
            if (invalidRoles.Any())
            {
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = $"The following roles do not exist: {string.Join(", ", invalidRoles)}"
                };
            }
            // Get existing roles
            var existingRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = request.RoleName.Except(existingRoles).ToList();

            // Assign roles
            var result = await _userManager.AddToRolesAsync(user, rolesToAdd);

            if (!result.Succeeded)
            {
                return new ManagerBaseResponse<bool>
                {
                    IsSuccess = false,
                    Result = false,
                    Message = string.Join("; ", result.Errors.Select(e => e.Description))
                };
            }
            return new ManagerBaseResponse<bool>
            {
                IsSuccess = true,
                Result = true,
                Message = "Roles assigned successfully."
            };
        }
    }
}
