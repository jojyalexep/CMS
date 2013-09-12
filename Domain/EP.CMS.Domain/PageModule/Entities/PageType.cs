using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule.Entities
{
    public class PageType : Entity
    {
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual int TemplateId { get; set; }
        public virtual string Url { get; set; }
    }
}
