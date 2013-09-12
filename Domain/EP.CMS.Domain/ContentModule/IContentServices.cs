using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule
{
    public interface IContentServices
    {
        Content Create(byte[] file, string extension, int contentLength, string mimeType, int directory, string displayName);
        Content Create(ContentTypeEnum contentType);
        Content Add(Content content);
        bool Edit(Content content);
        bool Delete(int contentId);
        Content GetContent(int id);
        IEnumerable<Content> GetAll();
        IEnumerable<Content> GetAllOfType(ContentTypeEnum contentType);
        IEnumerable<ImageContent> GetAllImagesOfCategory(ImageCategoryEnum categoryType);
        IEnumerable<Content> GetDirectoryContents(int directoryId);
    }
}
