using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Files.Entities
{
    public enum FileTypeEnum : int
    {
        None = 0,
        Page = 1,
        Image = 2,
        ThumbImage = 3,
        Swf = 4,
        Log = 5,
        Docs = 6,
        Audios = 7,
        Videos = 8
    }
}
