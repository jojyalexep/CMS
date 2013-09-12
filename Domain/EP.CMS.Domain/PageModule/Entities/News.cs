using EP.CMS.Domain.ContentModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule.Entities
{
    public class News : Page
    {
        public virtual string Summary { get; set; }
        public virtual string SubTitle { get; set; }
        public virtual DateTime NewsDate { get; set; }
        public virtual int Ranking { get; set; }
        public virtual bool WhatsNew { get; set; }
        public virtual ImageContent NewsImage { get; set; }
    }
}
