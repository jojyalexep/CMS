using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule
{
    public class ContentServices : IContentServices
    {
        #region Members
        readonly IContentRepository _contentRepository;
        readonly IBinaryExtRepository _binaryExtRepository;
        #endregion


        public ContentServices(IContentRepository contentRepository, IBinaryExtRepository binaryExtRepository)
        {
            _contentRepository = contentRepository;
            _binaryExtRepository = binaryExtRepository;
        }

        #region IContentServices Members

        public Content Create(ContentTypeEnum contentType)
        {
            return ContentFactory.CreateNewContent(contentType);
        }

        public Content Create(byte[] file, string extension, int contentLength, string mimeType, int directory, string displayName)
        {
            return ContentFactory.CreateNewContent(file,extension ,contentLength,mimeType,directory,displayName);
        }

        public Content Add(Content content)
        {
            var newContent = new Content();
            _contentRepository.UnitOfWork.Begin();
            newContent = _contentRepository.Add(content);
            
            _binaryExtRepository.UpdateContent(content);
            _contentRepository.UnitOfWork.Commit();
            return newContent;

        }

        public bool Edit(Content content)
        {
            _contentRepository.UnitOfWork.Begin();
            _contentRepository.Modify(content);
           
            _binaryExtRepository.UpdateContent(content);
            _contentRepository.UnitOfWork.Commit();
            return true;
        }

        public bool Delete(int contentId)
        {
            var content = GetContent(contentId);
            _contentRepository.UnitOfWork.Begin();
            _contentRepository.Remove(content);
            _binaryExtRepository.UpdateContent(content);
            _contentRepository.UnitOfWork.Commit();

            return true;
        }

        public Content GetContent(int id)
        {
            return _contentRepository.Get(id);
        }

        public IEnumerable<Content> GetAll()
        {
            return _contentRepository.GetAll();
        }

        public IEnumerable<Content> GetAllOfType(ContentTypeEnum contentType)
        {
            return _contentRepository.GetFiltered(o => o.TypeId == (int)contentType);
        }


        public IEnumerable<ImageContent> GetAllImagesOfCategory(ImageCategoryEnum categoryType)
        {
            if (categoryType == ImageCategoryEnum.All)
            {
                return _contentRepository.GetFiltered(o => o.TypeId == (int)ContentTypeEnum.Image)
                    .Cast<ImageContent>();
            }
            else
                return _contentRepository.GetFiltered(o => o.TypeId == (int)ContentTypeEnum.Image)
                    .Cast<ImageContent>().Where(p => p.CategoryId == (int)categoryType);
        }

        public IEnumerable<Content> GetDirectoryContents(int directoryId)
        {
            return _contentRepository.GetFiltered(p => p.DirectoryId == directoryId);
        }

        

        #endregion

    }
}
