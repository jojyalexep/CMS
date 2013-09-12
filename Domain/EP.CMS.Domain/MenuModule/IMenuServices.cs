using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.MenuModule.Entities;
using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.MenuModule
{
    public interface IMenuServices
    {
        Menu Create(Language language, int parentId);
        Menu Add(Menu menu, bool updateExtRepository = true);
        bool Edit(Menu menu, bool updateExtRepository = true);
        bool EditRefId(int menuId, int refId);
        bool DeleteRefForMenu(int refId);
        bool Delete(int menuId);
        bool DeleteAll(int[] idList);
        bool Publish(int menuId);
        bool Unpublish(int menuId);
        Menu GetMenu(int menuId);
        Menu GetLinkedMenu(int linkedId, Language language = Language.Arabic);
        bool UpdateMenuComplete(int menuId);
        IEnumerable<Menu> GetAll();
        bool ReorderMenu(List<int> idList);
    }
}
