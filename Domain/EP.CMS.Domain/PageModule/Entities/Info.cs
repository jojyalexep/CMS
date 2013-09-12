
using EP.CMS.Domain.ContentModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule.Entities
{
    public class Info : Page
    {
        public virtual string Summary { get; set; }
        public virtual ImageContent Banner { get; set; }
    }
}
