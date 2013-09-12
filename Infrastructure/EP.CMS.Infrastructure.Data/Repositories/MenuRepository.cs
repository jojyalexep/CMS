using EP.CMS.Domain.MenuModule;
using EP.CMS.Domain.MenuModule.Entities;
using EP.CMS.Infrastructure.Data.SeedWork;
using EP.CMS.Infrastructure.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository()
        {
        }
    }
}
