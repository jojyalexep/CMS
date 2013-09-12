using EP.CMS.Domain.UserModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.UserModule
{
    public class UserServices : IUserServices
    {
        IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #region IUserServices Members

        public bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);
            return ValidateUser(username, password, user);
        }

        public bool ValidateUser(string username, string password, EPUser user)
        {
            if (user != null && user.Username == username && user.Password == password)
                return true;
            return false;
        }

        public EPUser GetUser(string username)
        {
            return _userRepository.GetFiltered(p=>p.Username == username).FirstOrDefault();
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = GetUser(username);
            if (ValidateUser(username, oldPassword, user))
            {
                user.Password = newPassword;
                _userRepository.Modify(user);
                return true;
            }
            return false;
        }

        #endregion
    }
}
