using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.MenuModule.Entities
{
    public class Menu : Entity
    {
        public virtual string Name { get; set; }
        public virtual string LinkedUrl { get; set; }
        public virtual int ParentId { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual bool IsLeaf { get; set; }
        public virtual bool IsExternalRef { get; set; }
        public virtual int PageType { get; set; }
        public virtual int RefId { get; set; }
        public virtual int LanguageId { get; set; }
        public virtual int LinkedId { get; set; }
        public virtual int Ranking { get; set; }
        public virtual int StatusId { get; set; }
        public virtual IList<Menu> ChildMenu { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }

    }
}
