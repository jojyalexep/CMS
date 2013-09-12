



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
    public static class PageFactory
    {
        public static void CreateDefaultPages()
        {
            PageType page = new PageType { Id = 1, Name = "Home", DisplayName = "", TemplateId = 1, Url = "{0}/index.html" };
            PageType page1 = new PageType { Id = 2, Name = "Info", DisplayName = "", TemplateId = 1, Url = "{0}/info.xhtml" };
            PageType page2 = new PageType { Id = 3, Name = "News", DisplayName = "", TemplateId = 2, Url = "{0}/news.xhtml" };
            PageType page3 = new PageType { Id = 4, Name = "FAQ", DisplayName = "", TemplateId = 3, Url = "{0}/faq.xhtml" };
            PageType page4 = new PageType { Id = 5, Name = "TAC", DisplayName = "", TemplateId = 4, Url = "{0}/tac.xhtml" };
            PageType page5 = new PageType { Id = 6, Name = "Search", DisplayName = "", TemplateId = 5, Url = "{0}/search.xhtml" };
            PageType page6 = new PageType { Id = 6, Name = "Static", DisplayName = "Page", TemplateId = 5, Url = "{0}/search.xhtml" };
        }


        public static Page CreateNewPage(PageTypeEnum pageType, Language language, int linkedId)
        {
            if (pageType == PageTypeEnum.News)
            {
                var page = new News();
                page.Id = 0;
                page.LinkedId = linkedId;
                page.NewsDate = DateTime.Now;
                page.PublishedDate = DateTime.Now;
                page.CreatedDate = DateTime.Now;
                page.TypeId = (int)pageType;
                page.NewsImage = (ImageContent)ContentFactory.CreateNewContent(ContentTypeEnum.Image);
                page.LanguageId = (int)language;
                return page;
            }
            else if (pageType == PageTypeEnum.Info)
            {
                var page = new Info();
                page.Id = 0;
                page.LinkedId = linkedId;
                page.PublishedDate = DateTime.Now;
                page.CreatedDate = DateTime.Now;
                page.TypeId = (int)pageType;
                page.Banner = (ImageContent)ContentFactory.CreateNewContent(ContentTypeEnum.Image);
                page.LanguageId = (int)language;
                return page;
            }

            return new Page();


        }

    }
}
