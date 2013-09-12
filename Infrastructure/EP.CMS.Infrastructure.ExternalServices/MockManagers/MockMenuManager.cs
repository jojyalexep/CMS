
using EP.CMS.Domain.MenuModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Managers
{
    public class MockMenuManager : IMenuExtRepository
    {
        #region IMenuExtRepository Members

        public string UpdateTopMenu(Domain.MenuModule.Entities.Menu menu)
        {
            return String.Empty;
        }

        #endregion
    }
}
