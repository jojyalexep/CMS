using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain
{
    public class Entity
    {
        public virtual int Id { get; set; }
        public virtual bool IsDeleted { get; set; }

        public virtual bool IsTransient()
        {
            return this.Id == 0;
        }
    }
}
