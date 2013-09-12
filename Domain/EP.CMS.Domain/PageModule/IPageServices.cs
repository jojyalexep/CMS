using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule
{
    public interface IPageServices
    {
        Page Create(PageTypeEnum pageType, Language language, int linkedId = 0);
        Page Add(Page page, bool updateExtRepository = true);
        News AddNews(News news, int imageId);
        bool Edit(Page page);
        bool EditNews(News news, int imageId);
        bool EditInfo(Info info);
        bool EditAll(IEnumerable<Page> pageList, bool updateExtRepository = true);
        bool UpdateNewsWhatsNew(int id);
        bool UpdateInfoImage(int id, int imageId);
        bool Delete(int pageId);
        bool Publish(int pageId);
        bool Unpublish(int pageId);
        Page GetPage(int id);
        Info GetInfo(int id, Language language = Language.English);
        bool IsInfoComplete(int id);
        News GetNews(int id);
        Page GetLinkedPage(int id, Language language = Language.Arabic);
        IEnumerable<Page> GetAll();
        IEnumerable<Page> GetAllOfType(PageTypeEnum pageType);
        IEnumerable<Page> GetAllOfType(PageTypeEnum pageType, Language language);
        IEnumerable<Page> GetAllOfTypeWithFullName(PageTypeEnum pageType, string query);
    }
}
