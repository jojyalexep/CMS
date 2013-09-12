using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.PageModule;
using EP.CMS.Domain.PageModule.Entities;
using EP.CMS.Presentation.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EP.CMS.Presentation.Web.Controllers
{
    [Authorize]
    [HandleErrorWithELMAH]
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public IPageServices _pageServices;

        public NewsController(IPageServices pageServices)
        {
            _pageServices = pageServices;
        }



        public ActionResult Index(string query = "")
        {
            var newsList = _pageServices.GetAllOfTypeWithFullName(PageTypeEnum.News, query).Cast<News>();

            return View(newsList);
        }


        public ActionResult Edit(int newsId = 0)
        {
            News news = new News();
            if (newsId == 0)
            {
                news = (News)_pageServices.Create(PageTypeEnum.News, Domain.CommonModule.Enum.Language.English);
                ViewBag.ArabicModel = (News)_pageServices.Create(PageTypeEnum.News, Domain.CommonModule.Enum.Language.Arabic);
                ViewBag.Title = "News Manager: Add News";
            }
            else
            {
                news = (News)_pageServices.GetNews(newsId);
                var arabicNews = (News)_pageServices.GetLinkedPage(newsId, Domain.CommonModule.Enum.Language.Arabic);
                ViewBag.ArabicModel = arabicNews == null ? _pageServices.Create(PageTypeEnum.News, Language.Arabic, newsId) : arabicNews;
                ViewBag.Title = "News Manager: Edit News";
            }
            return View(news);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(News news)
        {

            news.CreatedDate = DateTime.Now;
            news.CreatedBy = User.Identity.Name;

            var action = ToolbarActionManager.GetAction();
            int imageId = Convert.ToInt32(Request.Form["ImageId"]);
            if (news.Id == 0)
            {
                _pageServices.AddNews(news, imageId);
            }
            else
            {
                news = (News)_pageServices.GetPage(news.Id);
                UpdateModel(news);
                _pageServices.EditNews(news, imageId);
            }
            return View(news);
        }

        public ActionResult Delete(int[] idList)
        {
            foreach (var id in idList)
            {
                _pageServices.Delete(id);
            }

            return Json(new { Success = true });
        }

        public ActionResult Publish(int[] idList)
        {
            foreach (var id in idList)
            {
                _pageServices.Publish(id);
            }

            return Json(new { Success = true });
        }

        public ActionResult Unpublish(int[] idList)
        {
            foreach (var id in idList)
            {
                _pageServices.Unpublish(id);
            }

            return Json(new { Success = true });
        }


        public ActionResult WhatsNew(int id)
        {
            var response = _pageServices.UpdateNewsWhatsNew(id);
            if (response)
            {
                return Json(new { Success = true, Message = "<h1> Updated What's New </h1><em>News Id :" + id + "</em>" });
            }
            return Json(new { Success = true, Message = "Cannot update What's New!!!" });
        }
    }
}
