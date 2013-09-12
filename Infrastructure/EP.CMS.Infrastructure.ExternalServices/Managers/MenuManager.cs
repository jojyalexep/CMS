using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.MenuModule;
using EP.CMS.Domain.MenuModule.Entities;
using EP.CMS.Infrastructure.ExternalServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Managers
{
    public class MenuManager : IMenuExtRepository
    {
        RepoService.English.CMSRepositorySEIClient englishService;
        RepoService.Arabic.CMSRepositorySEIClient arabicService;
        public MenuManager()
        {
        }

        public string UpdateTopMenu(Menu menu)
        {
            englishService = new RepoService.English.CMSRepositorySEIClient();
            arabicService = new RepoService.Arabic.CMSRepositorySEIClient();

            string response = String.Empty;
            if (menu.LanguageId == (int)Language.English)
            {
                response = englishService.opn_updateMenu("", GetEnglishMenu(menu));
            }
            else
            {
                response = arabicService.opn_updateMenu("", GetArabicMenu(menu));
            }
            WSReponseHandler.Handle(response);
            return response;

        }

        private RepoService.English.menuModel GetEnglishMenu(Menu menu)
        {
            if (menu.StatusId != (int)Status.Raw)
            {
                var englishMenu = new RepoService.English.menuModel();
                englishMenu.contextID = "GLOBAL";
                englishMenu.menuInstance = menu.Id == 1 ? "root" : "menu" + menu.Id.ToString();
                englishMenu.displayName = System.Security.SecurityElement.Escape(menu.DisplayName);
                englishMenu.menuID = menu.Name;
                englishMenu.leaf = menu.IsLeaf;
                englishMenu.linkedArticleID = menu.RefId == 0 ? null : "article" + menu.RefId.ToString();
                englishMenu.urlReference = menu.LinkedUrl;
                englishMenu.level = menu.Ranking;
                englishMenu.status = StatusUtil.GetStatusString(menu.StatusId, menu.IsDeleted);
                List<RepoService.English.menuModel> childMenuList = new List<RepoService.English.menuModel>();
                if (menu.ChildMenu != null)
                {
                    foreach (var childMenu in menu.ChildMenu.OrderBy(p => p.Ranking))
                    {
                        var newMenu = GetEnglishMenu(childMenu);
                        if (newMenu != null)
                            childMenuList.Add(newMenu);
                    }
                }
                englishMenu.childMenuList = childMenuList.ToArray();

                return englishMenu;
            }
            else return null;
        }

        private RepoService.Arabic.menuModel GetArabicMenu(Menu menu)
        {
            if (menu.StatusId != (int)Status.Raw)
            {
                var arabicMenu = new RepoService.Arabic.menuModel();

                arabicMenu.contextID = "GLOBAL";
                arabicMenu.menuInstance = menu.LinkedId == 1 ? "root" : "menu" + menu.LinkedId.ToString();
                arabicMenu.displayName = System.Security.SecurityElement.Escape(menu.DisplayName);
                arabicMenu.menuID = menu.Name;
                arabicMenu.leaf = menu.IsLeaf;
                arabicMenu.linkedArticleID = menu.RefId == 0 ? null : "article" + menu.RefId.ToString();
                arabicMenu.urlReference = menu.LinkedUrl;
                arabicMenu.level = menu.Ranking;
                arabicMenu.status = StatusUtil.GetStatusString(menu.StatusId, menu.IsDeleted);
                List<RepoService.Arabic.menuModel> childMenuList = new List<RepoService.Arabic.menuModel>();
                if (menu.ChildMenu != null)
                {
                    foreach (var childMenu in menu.ChildMenu.OrderBy(p => p.Ranking))
                    {
                        var newMenu = GetArabicMenu(childMenu);
                        if (newMenu != null)
                            childMenuList.Add(newMenu);
                    }
                }
                arabicMenu.childMenuList = childMenuList.ToArray();
                return arabicMenu;
            }
            else return null;
        }
    }
}
