using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.MenuModule;
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
    public class InfoController : Controller
    {
        public IPageServices _pageServices;

        public InfoController(IPageServices pageServices)
        {
            _pageServices = pageServices;
        }



        public ActionResult Index(string query = "")
        {
            var infoList = _pageServices.GetAllOfTypeWithFullName(PageTypeEnum.Info, query).Cast<Info>().OrderByDescending(p=>p.Id);
            return View(infoList);
        }

        //public ActionResult IndexT(string query = "")
        //{
        //    List<TimeSpan> timeTaken = new List<TimeSpan>();
        //    for (int i = 1; i < 20; i++)
        //    {
        //        var now = DateTime.Now;
        //        var infoList = _pageServices.GetAllOfTypeWithFullName(PageTypeEnum.Info, query).Cast<Info>().OrderByDescending(p => p.Id);
        //        timeTaken.Add(DateTime.Now.Subtract(now));
        //    }
        //    return View();
        //}

        public ActionResult Edit(int infoId = 0)
        {
            Info info = new Info();
            ViewBag.Language = Request["lang"] == "ar" ? Language.Arabic : Language.English;
            if (infoId == 0)
            {
                info = (Info)_pageServices.Create(PageTypeEnum.Info, Domain.CommonModule.Enum.Language.English);
                ViewBag.ArabicModel = (Info)_pageServices.Create(PageTypeEnum.Info, Domain.CommonModule.Enum.Language.Arabic);
                ViewBag.Title = "Article Manager: Add Article";
            }
            else
            {
                info = (Info)_pageServices.GetPage(infoId);
                var arabicInfo = (Info)_pageServices.GetLinkedPage(infoId, Domain.CommonModule.Enum.Language.Arabic);
                ViewBag.ArabicModel = arabicInfo == null ? _pageServices.Create(PageTypeEnum.Info, Language.Arabic, infoId) : arabicInfo;
                ViewBag.Title = "Article Manager: Edit Article";
            }
            return View(info);
        }


        public ActionResult UpdateInfoBanner(int infoId, int imageId, int menuId)
        {
            var response = _pageServices.UpdateInfoImage(infoId, imageId);
            var _menuServices = DependencyResolver.Current.GetService<IMenuServices>();
            bool menuComplete = _menuServices.UpdateMenuComplete(menuId);
            return Json(new { Success = response, MenuComplete = menuComplete }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Info info)
        {
            info.CreatedDate = DateTime.Now;
            info.CreatedBy = User.Identity.Name;

            var action = ToolbarActionManager.GetAction();
            if (info.Id == 0)
            {
                info = (Info)_pageServices.Add(info);
            }
            else
            {
                info = (Info)_pageServices.GetPage(info.Id);
                UpdateModel(info);
                _pageServices.EditInfo(info);
            }

            string workflow = Request["Workflow"];
            if (workflow == "menu_article_add" || workflow == "menu_article_edit")
            {
                int infoId = info.IsLinked() ? info.LinkedId : info.Id;
                    int menuId = Convert.ToInt32(Request["MenuId"]);
                var _menuServices = DependencyResolver.Current.GetService<IMenuServices>();
                if (workflow == "menu_article_add")
                    _menuServices.EditRefId(menuId, infoId);
                _menuServices.UpdateMenuComplete(menuId);
            }


            return Json(new { Success = true, id = info.Id, info = info });
        }

        public ActionResult Delete(int[] idList)
        {
            var _menuServices = DependencyResolver.Current.GetService<IMenuServices>();
            foreach (var id in idList)
            {
                _pageServices.Delete(id);
                _menuServices.DeleteRefForMenu(id);
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
    }
}
