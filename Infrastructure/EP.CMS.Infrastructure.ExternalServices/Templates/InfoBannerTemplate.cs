using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Templates
{
    internal static class InfoBannerTemplate
    {
        public static string GetContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            sb.Append("<div class=\"banner-title-left f-left\">");
            sb.Append("<h1>{0}</h1>");
            sb.Append("</div>");
            sb.Append("<div class=\"banner-right f-right\">");
            sb.Append(" <img src=\"_IMAGE_GALLERY_PATH_/content{1}\" height=\"200\" width=\"791\" alt=\"\" />");
            sb.Append("</div>");
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}
