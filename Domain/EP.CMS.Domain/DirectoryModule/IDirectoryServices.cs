using EP.CMS.Domain.DirectoryModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.DirectoryModule
{
    public interface IDirectoryServices
    {
        Directory Create();
        Directory Add(Directory directory);
        bool Edit(Directory directory);
        bool Delete(int directoryId);
        bool Delete(string directoryName);
        Directory GetDirectory(int id);
        Directory GetDirectory(string name);
        IEnumerable<Directory> GetAll();

    }
}
