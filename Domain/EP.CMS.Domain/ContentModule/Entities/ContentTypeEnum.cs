using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.ContentModule.Entities
{
    public enum ContentTypeEnum : int
    {
        Default = 0,
        Image = 2,
        Banner = 3,
        Swf = 4,
        
        Docs = 6,
        Audios = 7,
        Videos = 8
    }
}
