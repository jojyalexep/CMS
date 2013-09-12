using EP.CMS.Domain.UserModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.UserModule
{
    public static class UserSessionService
    {
        public static string GetLoggedUserRole(string userCookie)
        {
            return GetLoggedUserData(userCookie).Role;
        }

        public static bool UserInRole(string role, string userCookie)
        {
            if (GetLoggedUserData(userCookie).Role == role)
                return true;
            return false;
        }

        public static EPUser GetLoggedUserData(string userCookie)
        {
            var userData = userCookie.Split(','); // [0] - Username, [1] - Role
            return new EPUser { Username = userData[0], Role = userData[1] };
        }
    }
}
