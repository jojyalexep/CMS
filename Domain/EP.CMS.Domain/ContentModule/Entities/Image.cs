
using EP.CMS.Infrastructure.Files.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.ContentModule.Entities
{
    public class ImageContent : Content
    {
        public virtual int CategoryId { get; set; }
        public virtual string ThumbnailUrl
        {
            get
            {
                return new ContentManager().GetHttpPath(base.Id, base.Extension, 3);
            }
        }
    }
}
