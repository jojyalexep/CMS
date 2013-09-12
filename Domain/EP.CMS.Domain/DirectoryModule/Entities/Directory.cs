using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.DirectoryModule.Entities
{
    public class Directory : Entity
    {
        public virtual string Name { get; set; }
        public virtual int ParentId { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual IList<Directory> ChildDirectory { get; set; }
        public virtual bool CanModify { get; set; }
    }
}
