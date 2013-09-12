using EP.CMS.Domain.MenuModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.MenuModule
{
    public interface IMenuExtRepository
    {
        string UpdateTopMenu(Menu menu);
    }
}
