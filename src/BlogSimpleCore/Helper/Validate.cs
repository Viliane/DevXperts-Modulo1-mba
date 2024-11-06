using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSimpleCore.Helper
{
    public static class Validate
    {
        public static bool IsValidateUser(ClaimsPrincipal user, string AuthorId)
        {
            if (user.Identity.IsAuthenticated)
            {
                if (user.FindFirst(ClaimTypes.NameIdentifier).Value == AuthorId || (user.FindFirst(ClaimTypes.Role) != null && user.FindFirst(ClaimTypes.Role).Value == "Admin"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
