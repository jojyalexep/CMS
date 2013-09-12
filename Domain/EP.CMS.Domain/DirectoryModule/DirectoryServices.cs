using EP.CMS.Domain.DirectoryModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.DirectoryModule
{
    public class DirectoryServices : IDirectoryServices
    {
        #region Members
        readonly IDirectoryRepository _directoryRepository;
        #endregion


        public DirectoryServices(IDirectoryRepository directoryRepository)
        {
            _directoryRepository = directoryRepository;
        }

        #region IDirectoryServices Members

        public Directory Create()
        {
            return new Directory();
        }

        public Directory Add(Directory directory)
        {
            var newDirectory = new Directory();
            _directoryRepository.UnitOfWork.Begin();
            newDirectory = _directoryRepository.Add(directory);
            _directoryRepository.UnitOfWork.Commit();
            return newDirectory;
        }

        public bool Edit(Directory directory)
        {
            _directoryRepository.UnitOfWork.Begin();
            _directoryRepository.Modify(directory);
            _directoryRepository.UnitOfWork.Commit();

            return true;
        }

        public bool Delete(int directoryId)
        {
            var directory = GetDirectory(directoryId);
            return Delete(directory);
        }

        public bool Delete(string directoryName)
        {
            var directory = GetDirectory(directoryName);
            return Delete(directory);
        }

        public bool Delete(Directory directory)
        {
            if (directory != null && directory.CanModify)
            {
                _directoryRepository.UnitOfWork.Begin();
                _directoryRepository.SoftRemove(directory);
                _directoryRepository.UnitOfWork.Commit();
                return true;
            }
            return false;
        }

        public Directory GetDirectory(int id)
        {
            return _directoryRepository.Get(id);
        }

        public Directory GetDirectory(string name)
        {
            return _directoryRepository.GetFiltered(p=>p.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public IEnumerable<Directory> GetAll()
        {
            return _directoryRepository.GetAll();
        }

        #endregion
    }
}
