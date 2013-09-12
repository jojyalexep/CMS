using EP.CMS.Domain.ContentModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EP.CMS.Presentation.Web.Utilities
{
    public class ImageManager
    {
        public ImageContent GetImageFromRequest(string name)
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Files[0];
                ImageContent image = new ImageContent();
                image.ContentLength = file.ContentLength;
                image.CreatedDate = DateTime.Now;
                image.DisplayName = name;
                image.Extension = image.Extension;
                //image.

                return image;
            }
            else
            {
                throw new ArgumentNullException("No Files");
            }
        }
    }
}