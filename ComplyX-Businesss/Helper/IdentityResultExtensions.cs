
using ComplyX_Businesss.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.ComponentModel;

namespace ComplyX.Shared.Helper
{
    public static class IdentityResultExtensions
    {
         
        public static string GetUserId(this IEnumerable<Claim> claims)
        {
            var findUserId = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            if (findUserId != null && !string.IsNullOrWhiteSpace(findUserId.Value))
            {
                return findUserId.Value;
            }
            //TODO: handle this
            return "-1";
        }

        public static string GetUserName(this IEnumerable<Claim> claims)
        {
            var findUserId = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();
            if (findUserId != null && !string.IsNullOrWhiteSpace(findUserId.Value))
            {
                return findUserId.Value;
            }
            //TODO: handle this
            return "-1";
        }

       
    }

}
