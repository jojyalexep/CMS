using EP.CMS.Domain.MenuModule.Entities;
using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.CMS.Domain.PageModule;

namespace EP.CMS.Domain.MenuModule.Services
{
    public class MenuServices : IMenuServices
    {
        #region Members
        readonly IMenuRepository _menuRepository;
        readonly IMenuExtRepository _menuExtRepository;
        readonly IPageServices _pageServices;
        #endregion


        public MenuServices(IMenuRepository menuRepository, IMenuExtRepository menuExtRepository, IPageServices pageServices)
        {
            _menuRepository = menuRepository;
            _menuExtRepository = menuExtRepository;
            _pageServices = pageServices;
        }

        #region IMenuServices Members

        public Menu Create(Language language, int parentId)
        {
            return new Menu { LanguageId = (int)language, CreatedDate = DateTime.Now, StatusId = (int)Status.Draft, ParentId = parentId };
        }

        public Menu Add(Menu menu, bool updateExtRepository = true)
        {
            menu.CreatedDate = DateTime.Now;
            _menuRepository.UnitOfWork.Begin();
            menu = _menuRepository.Add(menu);

            if (updateExtRepository)
                UpdateExternalRepository();
            _menuRepository.UnitOfWork.Commit();
            return menu;
        }



        public bool Edit(Menu menu, bool updateExtRepository = true)
        {
            _menuRepository.UnitOfWork.Begin();
            _menuRepository.Modify(menu);

            if (updateExtRepository)
                UpdateExternalRepository();
            _menuRepository.UnitOfWork.Commit();
            return true;
        }

        public bool Delete(int menuId)
        {
            var menu = GetMenu(menuId);
            if (menu.StatusId != (int)Status.Published)
            {

                var linkedMenu = GetLinkedMenu(menuId, Language.Arabic);
                _menuRepository.UnitOfWork.Begin();
                _menuRepository.SoftRemove(menu);

                UpdateExternalRepository();
                _menuRepository.UnitOfWork.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAll(int[] idList)
        {
            _menuRepository.UnitOfWork.Begin();
            foreach (var menuId in idList)
            {
                var menu = GetMenu(menuId);
                if (menu.StatusId != (int)Status.Published) // CHANGE TO PUBLISH
                {
                    var linkedMenu = GetLinkedMenu(menuId, Language.Arabic);
                    _menuRepository.SoftRemove(menu);
                    _menuRepository.SoftRemove(linkedMenu);
                }
                else
                {
                    return false;
                }
            }

            UpdateExternalRepository();
            _menuRepository.UnitOfWork.Commit();
            return true;

        }

        public Menu GetMenu(int menuId)
        {
            return _menuRepository.Get(menuId);
        }

        public bool IsMenuComplete(Menu menu)
        {
            return _pageServices.IsInfoComplete(menu.RefId);
        }


        public bool UpdateMenuComplete(int menuId)
        {
            var menu = GetMenu(menuId);
            if (menu.StatusId == (int)Status.Raw)
            {
                if (menu.IsLeaf && IsMenuComplete(menu))
                {
                    var linkedMenu = GetLinkedMenu(menuId);
                    if (linkedMenu != null)
                    {
                        linkedMenu.StatusId = (int)Status.Draft;
                        Edit(linkedMenu, false);
                    }
                    menu.StatusId = (int)Status.Draft;
                    return Edit(menu);

                }
            }
            else
            {
                return true;
            }
            return false;
        }

        public bool DeleteRefForMenu(int refId)
        {
            var menu = _menuRepository.GetFiltered(p => p.RefId == refId).FirstOrDefault();
            if (menu != null)
            {
                EditRefId(menu.Id, 0);
            }
            return false;
        }

        public Menu GetLinkedMenu(int linkedId, Language language = Language.Arabic)
        {
            var menus = _menuRepository.GetFiltered(o => o.LinkedId == linkedId && o.LanguageId == (int)language);
            if (menus.Any())
            {
                return menus.FirstOrDefault();
            }
            return null;
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        #endregion


        #region IMenuServices Members


        public bool ReorderMenu(List<int> idList)
        {
            int rank = 1;
            _menuRepository.UnitOfWork.Begin();
            foreach (var id in idList)
            {
                var menu = GetMenu(id);
                var linkedMenu = GetLinkedMenu(id);
                menu.Ranking = rank;
                linkedMenu.Ranking = rank;
                _menuRepository.Modify(menu);
                _menuRepository.Modify(linkedMenu);
                rank++;
            }

            UpdateExternalRepository();
            _menuRepository.UnitOfWork.Commit();
            return true;
        }


        public bool UpdateExternalRepository()
        {
            _menuExtRepository.UpdateTopMenu(GetMenu(1));
            _menuExtRepository.UpdateTopMenu(GetLinkedMenu(1));
            return true;
        }

        public bool EditRefId(int menuId, int refId)
        {
            var menu = GetMenu(menuId);
            var linkedMenu = GetLinkedMenu(menuId);
            linkedMenu.RefId = refId;
            menu.RefId = refId;
            Edit(linkedMenu, false);
            return Edit(menu, false);
        }

        #endregion


        #region IMenuServices Members


        public bool Publish(int menuId)
        {
            var menu = GetMenu(menuId);
            var linkedMenu = GetLinkedMenu(menuId, Language.Arabic);
            if (linkedMenu != null)
            {
                _menuRepository.UnitOfWork.Begin();
                menu.StatusId = (int)Status.Published;
                linkedMenu.StatusId = (int)Status.Published;
                _menuRepository.Modify(menu);
                _menuRepository.Modify(linkedMenu);
                _menuRepository.UnitOfWork.Commit();
                UpdateExternalRepository();
                return true;
            }
            return false;


        }

        public bool Unpublish(int menuId)
        {
            var menu = GetMenu(menuId);
            var linkedMenu = GetLinkedMenu(menuId, Language.Arabic);
            if (linkedMenu != null && menu.StatusId != (int)Status.Raw && linkedMenu.StatusId != (int)Status.Raw)
            {
                _menuRepository.UnitOfWork.Begin();
                menu.StatusId = (int)Status.UnPublished;
                linkedMenu.StatusId = (int)Status.UnPublished;
                _menuRepository.Modify(menu);
                _menuRepository.Modify(linkedMenu);
                _menuRepository.UnitOfWork.Commit();
                UpdateExternalRepository();
                return true;
            }
            return false;
        }

        #endregion
    }
}
