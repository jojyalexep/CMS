using EP.CMS.Domain.DirectoryModule;
using EP.CMS.Domain.DirectoryModule.Entities;
using EP.CMS.Infrastructure.Data.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.Repositories
{
    public class DirectoryRepository : Repository<Directory>, IDirectoryRepository
    {
    }
}
