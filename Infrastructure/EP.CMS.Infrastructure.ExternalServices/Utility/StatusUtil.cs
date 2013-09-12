using EP.CMS.Domain.CommonModule.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Utility
{
    internal static class StatusUtil
    {
        public static string GetStatusString(int statusId, bool deleted = false)
        {
            if (deleted)
                return "DELETE";
            else
            {
                switch (statusId)
                {
                    case (int)Status.Raw:
                        {
                            return "NEW";
                        }
                    case (int)Status.Draft:
                        {
                            return "NEW";
                        }
                    case (int)Status.Published:
                        {
                            return "PUBLISH";
                        }
                    case (int)Status.UnPublished:
                        {
                            return "UNPUBLISH";
                        }
                }
            }
            return "";
        }
    }
}

