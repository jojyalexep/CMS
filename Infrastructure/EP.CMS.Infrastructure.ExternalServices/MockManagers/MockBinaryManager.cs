using EP.CMS.Domain.ContentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Managers
{
    public class MockBinaryManager : IBinaryExtRepository
    {
        #region IBinaryExtRepository Members

        public string UpdateContent(Domain.ContentModule.Entities.Content content)
        {
            return String.Empty;
        }

        #endregion
    }
}
