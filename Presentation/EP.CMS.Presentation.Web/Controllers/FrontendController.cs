using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.MenuModule;
using EP.CMS.Domain.PageModule;
using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EP.CMS.Presentation.Web.Controllers
{
    public class FrontendController : Controller
    {
        //
        // GET: /Frontend/


         public IPageServices _pageServices;
         public IContentServices _contentServices;
         public IMenuServices _menuServices;

         public FrontendController(IPageServices pageServices, IContentServices contentServices, IMenuServices menuServices)
        {
            _pageServices = pageServices;
            _contentServices = contentServices;
            _menuServices = menuServices;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UpdateImages()
        {
            var images = _contentServices.GetAllOfType(Domain.ContentModule.Entities.ContentTypeEnum.Image);
            foreach (var image in images)
            {
                var imageData = _contentServices.GetContent(image.Id);
                _contentServices.Edit(imageData);
            }
            return Json(new { Success = true}, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UpdateArticle()
        {
            var articles = _pageServices.GetAllOfType(Domain.PageModule.Entities.PageTypeEnum.Info);
            foreach (var article in articles)
            {
                try
                {
                    Info newArticle = _pageServices.GetInfo(article.Id);
                    newArticle.Banner = (ImageContent)_contentServices.GetContent(newArticle.Banner.Id);
                    _pageServices.EditInfo(newArticle);
                }
                catch (Exception ex)
                {
                    
                }
              
            }

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UpdateNews()
        {
            var newsList = _pageServices.GetAllOfType(Domain.PageModule.Entities.PageTypeEnum.News);
            foreach (var news in newsList)
            {
                var newNews = _pageServices.GetNews(news.Id);
                newNews.SubTitle = string.IsNullOrEmpty(newNews.SubTitle) ? "." : newNews.SubTitle;
                _pageServices.EditNews(newNews,newNews.NewsImage.Id);
            }

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult UpdateMenu()
        {
            _menuServices.Edit(_menuServices.GetMenu(1));
            _menuServices.Edit(_menuServices.GetLinkedMenu(1));
            
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateImageStatus()
        {
            var images = _contentServices.GetAllOfType(Domain.ContentModule.Entities.ContentTypeEnum.Image);
            foreach (var image in images)
            {
                var imageData = _contentServices.GetContent(image.Id);
                imageData.StatusId = 1;
                _contentServices.Edit(imageData);
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult CheckMenu(string lang)
        {
            Language language = lang == "en" ? Language.English : Language.Arabic;

            var menus = _menuServices.GetAll();
            foreach (var menu in menus)
            {

            }


            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}
