using AutoMapper;
using ComplyX.Data.DbContexts;
using ComplyX.Data.Entities;
using ComplyX.Repositories.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ComplyX.Repositories.Repositories
{
    public class UserRespositories(IMapper mapper, AppDbContext context, UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> roleManager) : IUserRespositories
    {

        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _context = context;
        private readonly UserManager<ApplicationUsers> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<IdentityResult> AddRoles(ApplicationUsers user, List<string> roleNames)
        {
            return await _userManager.AddToRolesAsync(user, roleNames);
        }



        public async Task<IdentityResult> CreateUser(ApplicationUsers user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> CreateUser(ApplicationUsers user)
        {
            return await _userManager.CreateAsync(user);
        }


        public async Task<ApplicationUsers?> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUsers?> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

#nullable enable
        public async Task<ApplicationUsers?> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }



        public async Task<IdentityResult> RemoveRoles(ApplicationUsers user, List<string> roleNames)
        {
            return await _userManager.RemoveFromRolesAsync(user, roleNames);
        }

        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }



        //public async Task<Dictionary<string, List<string>>> GetUserRolesByUserIds(IEnumerable<string> userIds)
        //{
        //    var pairs = await _context
        //        .Set<IdentityUserRole<string>>()
        //        .Where(ur => userIds.Contains(ur.UserId))
        //        .Join(_roleManager.Roles,
        //              ur => ur.RoleId,
        //              r => r.Id,
        //              (ur, r) => new { ur.UserId, RoleName = r.Name })
        //        .ToListAsync();

        //    return pairs
        //        .GroupBy(p => p.UserId)
        //        .ToDictionary(
        //            g => g.Key,
        //            g => g.Select(x => x.RoleName ?? string.Empty)
        //                  .Where(n => !string.IsNullOrEmpty(n))
        //                  .Distinct()
        //                  .ToList());
        //}

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

         
    }
}
