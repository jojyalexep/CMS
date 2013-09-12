using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.MenuModule;
using EP.CMS.Domain.MenuModule.Entities;
using EP.CMS.Domain.PageModule;
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
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        public IMenuServices _menuServices;


        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        public ActionResult Index(int id = 0)
        {
            ViewBag.MenuId = id;

            ViewBag.FrontEndInfoRaw = LinkManager.ViewFrontEndInfoRaw(EP.CMS.Domain.CommonModule.Enum.Language.English);
            var menuList = _menuServices.GetMenu(1);
            var allMenu = _menuServices.GetAll();
            menuList = UpdateMenuName(menuList, allMenu);
            return View(menuList);
        }

        public ActionResult Static(string id)
        {
            ViewBag.MenuId = id;
            return View();
        }

        public ActionResult StaticContent(string page)
        {
            int infoId = 0;
            switch (page)
            {

                case "Home":
                    {
                        var contents = DependencyResolver.Current.GetService<IContentServices>().GetAllImagesOfCategory(ImageCategoryEnum.Home_Banner);
                        var englishContent = contents.Where(p => p.LanguageId == (int)Language.English);
                        ViewBag.ArabicModel = contents.Where(p => p.LanguageId == (int)Language.Arabic);
                        return View("Home", englishContent);
                    }

                case "FAQ":
                    {
                        var contents = DependencyResolver.Current.GetService<IContentServices>().GetAllImagesOfCategory(ImageCategoryEnum.FAQ_Banner);
                        var englishContent = contents.FirstOrDefault(p => p.LanguageId == (int)Language.English);
                        ViewBag.ArabicModel = contents.FirstOrDefault(p => p.LanguageId == (int)Language.Arabic);
                        return View("NoInfo", englishContent);
                    }
                case "TAC":
                    {
                        infoId = 84;
                        break;
                    }

                case "Disclaimer":
                    {
                        infoId = 85;
                        break;
                    }

                case "Privacy":
                    {
                        infoId = 86;
                        break;
                    }
                case "Search":
                    {
                        var contents = DependencyResolver.Current.GetService<IContentServices>().GetAllImagesOfCategory(ImageCategoryEnum.Search_Banner);
                        var englishContent = contents.FirstOrDefault(p => p.LanguageId == (int)Language.English);
                        ViewBag.ArabicModel = contents.FirstOrDefault(p => p.LanguageId == (int)Language.Arabic);
                        return View("NoInfo", englishContent);
                    }
                default:
                    {
                        break;
                    }
            }
            var _pageServices = DependencyResolver.Current.GetService<IPageServices>();
            ViewBag.Info = _pageServices.GetInfo(infoId);
            ViewBag.Action = "Static";
            ViewBag.ArabicModel = _pageServices.GetInfo(infoId, Language.Arabic);
            return View("Details");

        }



        private Menu UpdateMenuName(Menu menu, IEnumerable<Menu> allMenu)
        {
            int id = menu.Id;
            var arabicMenu = allMenu.First(p => p.LinkedId == id);
            menu.DisplayName += " / " + arabicMenu.DisplayName;
            menu.LinkedId = arabicMenu.Id;
            foreach (var childMenu in menu.ChildMenu)
            {
                UpdateMenuName(childMenu, allMenu);
            }
            return menu;
        }

        public ActionResult Edit(int menuId = 0, int parentId = 0)
        {
            Menu menu = new Menu();
            if (menuId == 0)
            {
                menu = _menuServices.Create(Language.English, parentId);
                ViewBag.ArabicModel = (Menu)_menuServices.Create(Language.Arabic, parentId);

            }
            else
            {
                menu = _menuServices.GetMenu(menuId);
                ViewBag.ArabicModel = _menuServices.GetLinkedMenu(menuId);
            }


            GetParentMenu(menu.ParentId);
            return View(menu);
        }

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {


            string arabicName = Request.Form["ArabicDisplayName"];
            if (menu.Id == 0)
            {
                menu.LanguageId = (int)Language.English;
                menu.Name = "mnu_" + menu.DisplayName.Replace(" ", "-");
                menu.CreatedBy = User.Identity.Name;
                menu.CreatedDate = DateTime.Now;
                menu.IsLeaf = true;
                menu.StatusId = (int)Status.Raw;
                var englishMenu = _menuServices.Add(menu, false);


                var arabicMenu = _menuServices.Create(Language.Arabic, _menuServices.GetLinkedMenu(menu.ParentId).Id);
                arabicMenu.LinkedId = englishMenu.Id;
                arabicMenu.DisplayName = arabicName;
                arabicMenu.Name = "mnu_" + menu.DisplayName.Replace(" ", "-");
                arabicMenu.CreatedBy = User.Identity.Name;
                arabicMenu.CreatedDate = DateTime.Now;
                arabicMenu.IsLeaf = true;
                arabicMenu.StatusId = (int)Status.Raw;
                arabicMenu = _menuServices.Add(arabicMenu, false);
            }
            else
            {
                menu = _menuServices.GetMenu(menu.Id);
                UpdateModel(menu);
                _menuServices.Edit(menu, false);

                var arabicMenu = _menuServices.GetLinkedMenu(menu.Id);
                arabicMenu.DisplayName = arabicName;
                arabicMenu.ParentId = _menuServices.GetLinkedMenu(menu.ParentId).Id;
                _menuServices.Edit(arabicMenu);
            }
            return Json(new { Success = true });
        }

        public ActionResult Details(int menuId = 0)
        {
            Menu menu = _menuServices.GetMenu(menuId);
            var _pageServices = DependencyResolver.Current.GetService<IPageServices>();

            var englishInfo = _pageServices.GetInfo(menu.RefId);
            ViewBag.Info = (englishInfo == null) ?
                _pageServices.Create(Domain.PageModule.Entities.PageTypeEnum.Info, Language.English) :
                englishInfo;
            var arabicInfo = _pageServices.GetInfo(menu.RefId, Language.Arabic);
            ViewBag.ArabicModel = (arabicInfo == null) ?
                _pageServices.Create(Domain.PageModule.Entities.PageTypeEnum.Info, Language.Arabic) :
                arabicInfo;
            ViewBag.Action = "Article";
            return View(menu);
        }

        private void GetParentMenu(int selectedId = 0)
        {
            List<Menu> menuList = _menuServices.GetAll().Where(p => p.IsLeaf == false).ToList();

            var newMenuList = menuList.Where(p=>p.LanguageId == (int)Language.English);
            foreach (var menu in newMenuList)
            {
                var arabicMenu = menuList.FirstOrDefault(p => p.LinkedId == menu.Id);
                if (arabicMenu != null)
                {
                    menu.DisplayName += " / " + arabicMenu.DisplayName;
                }
            }

            ViewBag.ParentMenu = new SelectList(newMenuList, "Id", "DisplayName", selectedId);
            //ViewBag.ArabicParentMenu = new SelectList(newMenuList, "LinkedId", "DisplayName");
        }

        public ActionResult Reorder(int id)
        {
            return View(_menuServices.GetMenu(id).ChildMenu.Where(p=>p.IsDeleted== false).OrderBy(p => p.Ranking));
        }

        public ActionResult SubmitOrder(List<int> idList)
        {
            _menuServices.ReorderMenu(idList);
            return Json(new { Success = true, Message = "Reordered successfully" });
        }

        private void GetMenuAsList(Menu menu, ref List<Menu> menuList)
        {
            menuList.Add(menu);
            if (menu.ChildMenu != null)
            {
                foreach (var childMenu in menu.ChildMenu)
                {
                    GetMenuAsList(childMenu, ref menuList);
                }
            }
        }

        public ActionResult Delete(int[] idList)
        {
            var response = _menuServices.DeleteAll(idList);
            return Json(new { Success = response });
        }

        public ActionResult Publish(int[] idList)
        {
            foreach (var id in idList)
            {
                _menuServices.Publish(id);
            }

            return Json(new { Success = true });
        }

        public ActionResult Unpublish(int[] idList)
        {
            foreach (var id in idList)
            {
                _menuServices.Unpublish(id);
            }

            return Json(new { Success = true });
        }

    }
}

