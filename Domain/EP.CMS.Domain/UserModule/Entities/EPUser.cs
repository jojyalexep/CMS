using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.UserModule.Entities
{
    public class EPUser : Entity
    {
        public virtual string Username { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual string Role { get; set; }
    }
}
