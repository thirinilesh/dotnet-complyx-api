using ComplyX.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX.Repositories.Repositories.Abstractions
{
    public interface IUserRespositories
    {
        Task<ApplicationUsers?> GetUserByUsername(string username);
        Task<ApplicationUsers?> GetUserByEmail(string email);
        Task<ApplicationUsers?> GetUserById(string userId);
        Task<IdentityResult> CreateUser(ApplicationUsers user, string password);
        Task<IdentityResult> CreateUser(ApplicationUsers user); 
        Task<IdentityResult> AddRoles(ApplicationUsers user, List<string> roleNames);
        Task<bool> RoleExists(string roleName);
        Task<IdentityResult> RemoveRoles(ApplicationUsers user, List<string> roleNames);
      //  Task<Dictionary<string, List<string>>> GetUserRolesByUserIds(IEnumerable<string> userIds);
        Task<int> SaveChangesAsync();
         
    }
}
