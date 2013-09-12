using EP.CMS.Domain.UserModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.UserModule
{
    public interface IUserServices
    {
        bool ValidateUser(string username, string password);
        EPUser GetUser(string username);
        bool ChangePassword(string username, string oldPassword, string newPassword);
    }
}
