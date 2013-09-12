using EP.CMS.Domain.ContentModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Utility
{
    internal static class ImageCategoryUtil
    {

        public static string GetCategoryName(int categoryId)
        {
            switch (categoryId)
            {
                case (int)ImageCategoryEnum.News:
                    {
                        return "images";
                    }
                case (int)ImageCategoryEnum.Banner:
                    {
                        return "banner";
                    }
                case (int)ImageCategoryEnum.Info_Banner:
                    {
                        return "images";
                    }

                case (int)ImageCategoryEnum.FAQ_Banner:
                    {
                        return "banner";
                    }

                case (int)ImageCategoryEnum.Search_Banner:
                    {
                        return "banner";
                    }

                case (int)ImageCategoryEnum.Home_Banner:
                    {
                        return "banner";
                    }

                default:
                    {
                        return "images";
                    }
            }
        }
    }
}
