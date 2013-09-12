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
    public class PageServices : IPageServices
    {
        #region Members
        readonly IPageRepository _pageRepository;
        readonly IContentServices _contentServices;
        readonly IPageExtRepository _pageExtRepository;
        #endregion


        public PageServices(IPageRepository pageRepository, IContentServices contentServices, IPageExtRepository pageExtRepository)
        {
            _pageRepository = pageRepository;
            _contentServices = contentServices;
            _pageExtRepository = pageExtRepository;
        }

        #region IPageServices Members


        public Page Create(PageTypeEnum pageType, Language language, int linkedId = 0)
        {
            return PageFactory.CreateNewPage(pageType, language, linkedId);
        }


        public Page Add(Page page, bool updateExtRepository = true)
        {
            if (!page.IsLinked() && page.LanguageId != (int)Language.English)
            {
                var linkPage = Create(page.PageType, Language.English);
                var englishPage = Add(linkPage, false);
                page.LinkedId = englishPage.Id;
            }

            if (page.GetType() == typeof(News))
            {
                var news = (News)page;
                page.TypeId = (int)PageTypeEnum.News;
                if (news.NewsImage != null && news.NewsImage.Id == 0) news.NewsImage = null;
            }

            else if (page.GetType() == typeof(Info))
            {
                var info = (Info)page;
                page.TypeId = (int)PageTypeEnum.Info;
                if (info.Banner != null && info.Banner.Id == 0) info.Banner = null;
            }
            if (!String.IsNullOrEmpty(page.Title))
                page.Name = page.Title.Replace(" ", "-").Replace(".", "-").ToLower();
            _pageRepository.UnitOfWork.Begin();
            var newPage = _pageRepository.Add(page);
            if (updateExtRepository)
                _pageExtRepository.UpdatePage(page);

            _pageRepository.UnitOfWork.Commit();
            return newPage;
        }

        public News AddNews(News news, int imageId)
        {
            news.NewsImage = (ImageContent)_contentServices.GetContent(imageId);
            Add(news, true);
            //UpdateNewsWhatsNew(news.Id);

            return news;

        }

        public bool Edit(Page page)
        {
            _pageRepository.UnitOfWork.Begin();
            _pageRepository.Modify(page);

            _pageExtRepository.UpdatePage(page);
            _pageRepository.UnitOfWork.Commit();

            return true;
        }

        public bool EditNews(News news, int imageId)
        {
            if (news.NewsImage.Id != imageId)
                news.NewsImage = (ImageContent)_contentServices.GetContent(imageId);
            return Edit(news);
        }

        public bool UpdateInfoImage(int id, int imageId)
        {
            var info = (Info)GetPage(id);
            var banner = (ImageContent)_contentServices.GetContent(imageId);
            UpdateInfoImage(info, banner);

            if (info.LinkedId == 0)
            {
                var linkedInfo = (Info)GetLinkedPage(id);
                if (linkedInfo != null && linkedInfo.Banner == null)
                {
                    UpdateInfoImage(linkedInfo, banner);
                }
            }

            return true;
        }

        public bool UpdateInfoImage(Info info, ImageContent banner)
        {
            info.Banner = banner;
            return EditInfo(info);
        }

        public bool EditInfo(Info info)
        {
            Edit(info);
            return true;
        }

        public bool UpdateNewsWhatsNew(int id)
        {
            List<News> newsList = new List<News>();
            foreach (News news in GetAllOfType(PageTypeEnum.News).Cast<News>().Where(p => p.WhatsNew))
            {
                news.WhatsNew = false;
                newsList.Add(news);
            }
            EditAll(newsList, false);

            var eNews = GetNews(id);
            eNews.WhatsNew = true;
            Edit(eNews);

            var linkedNews = (News)GetLinkedPage(id);
            if (linkedNews != null)
            {
                linkedNews.WhatsNew = true;
                Edit(linkedNews);
            }

            return true;
        }

        public bool EditAll(IEnumerable<Page> pageList, bool updateExtRepository = true)
        {
            _pageRepository.UnitOfWork.Begin();
            foreach (var page in pageList)
            {
                _pageRepository.Modify(page);
                if (updateExtRepository)
                    _pageExtRepository.UpdatePage(page);

            }
            _pageRepository.UnitOfWork.Commit();
            return true;
        }

        public bool Delete(int pageId)
        {
            var page = GetPage(pageId);
            var linkedPage = GetLinkedPage(pageId, Language.Arabic);
            _pageRepository.UnitOfWork.Begin();
            _pageRepository.SoftRemove(page);
            _pageExtRepository.UpdatePage(page);
            if (linkedPage != null)
            {
                _pageRepository.SoftRemove(linkedPage);
                _pageExtRepository.UpdatePage(linkedPage);
            }


            _pageRepository.UnitOfWork.Commit();

            return true;
        }

        public bool Publish(int pageId)
        {
            var page = GetPage(pageId);
            var linkedPage = GetLinkedPage(pageId, Language.Arabic);
            if (linkedPage != null)
            {
                _pageRepository.UnitOfWork.Begin();
                page.StatusId = (int)Status.Published;
                _pageRepository.Modify(page);
                _pageExtRepository.UpdatePage(page);
                linkedPage.StatusId = (int)Status.Published;
                _pageRepository.Modify(linkedPage);
                _pageExtRepository.UpdatePage(linkedPage);
                _pageRepository.UnitOfWork.Commit();
                return true;
            }
            return false;
        }

        public bool Unpublish(int pageId)
        {
            var page = GetPage(pageId);
            var linkedPage = GetLinkedPage(pageId, Language.Arabic);
            if (linkedPage != null)
            {
                _pageRepository.UnitOfWork.Begin();
                page.StatusId = (int)Status.UnPublished;
                _pageRepository.Modify(page);
                _pageExtRepository.UpdatePage(page);

                linkedPage.StatusId = (int)Status.UnPublished;
                _pageRepository.Modify(linkedPage);
                _pageExtRepository.UpdatePage(linkedPage);

                _pageRepository.UnitOfWork.Commit();
                return true;
            }
            return false;
        }

        public Page GetPage(int id)
        {
            return _pageRepository.Get(id);
        }



        public Info GetInfo(int id, Language language = Language.English)
        {

            var info = language == Language.English ? (Info)GetPage(id) : (Info)GetLinkedPage(id);
            if (info != null && info.Banner == null) info.Banner = (ImageContent)_contentServices.Create(ContentTypeEnum.Image);
            return info;
        }

        public bool IsInfoComplete(int id)
        {
            var info = (Info)GetPage(id);
            var linkedInfo = (Info)GetLinkedPage(id);
            if (info != null && info.Banner != null && linkedInfo != null && linkedInfo.Banner != null)
                return true;
            return false;
        }

        public News GetNews(int id)
        {
            var news = (News)GetPage(id);
            if (news.NewsImage == null) news.NewsImage = (ImageContent)_contentServices.Create(ContentTypeEnum.Image);
            return news;
        }

        public Page GetLinkedPage(int linkedId, Language language = Language.Arabic)
        {
            var pages = _pageRepository.GetFiltered(o => o.LinkedId == linkedId && o.LanguageId == (int)language);
            if (pages.Any())
            {
                return GetPage(pages.FirstOrDefault().Id);
            }
            return null;
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public IEnumerable<Page> GetAllOfType(PageTypeEnum pageType)
        {
            return _pageRepository.GetFiltered(o => o.TypeId == (int)pageType);
        }

        public IEnumerable<Page> GetAllOfTypeWithFullName(PageTypeEnum pageType, string query)
        {

            var pageList = _pageRepository.GetFiltered(o => o.TypeId == (int)pageType);
            var distinctPageList = pageList.Where(p => p.LanguageId == (int)Language.English).ToList();
            distinctPageList.ForEach(delegate(Page page)
            {
                var linkedPage = pageList.Where(p => p.LinkedId == page.Id).FirstOrDefault();
                if (linkedPage != null)
                {
                    page.Title += " / " + linkedPage.Title;
                }

            });
            if (!String.IsNullOrEmpty(query))
            {
                query = query.ToLower();
                distinctPageList = distinctPageList.Where(p => p.Title.ToLower().Contains(query)).ToList();
            }

            return distinctPageList;


            //var news = _pageRepository.GetFiltered(o => o.TypeId == 0).Cast<News>();
            //foreach (var newA in news)
            //{
            //    newA.TypeId = 5;
            //    EditNews(newA);
            //}

            //return news;
        }

        public IEnumerable<Page> GetAllOfType(PageTypeEnum pageType, Language language)
        {
            return _pageRepository.GetFiltered(o => o.TypeId == (int)pageType && o.LanguageId == (int)language);
        }


        #endregion
    }
}
