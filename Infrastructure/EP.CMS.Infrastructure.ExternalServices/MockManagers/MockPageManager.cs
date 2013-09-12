using EP.CMS.Domain.PageModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Managers
{
    public class MockPageManager : IPageExtRepository
    {
        #region IPageExtRepository Members

        public string UpdatePage(Domain.PageModule.Entities.Page page)
        {
            return String.Empty;
        }

        public string ReOrderNews(string[] newsIds)
        {
            return String.Empty;
        }

        #endregion
    }
}
