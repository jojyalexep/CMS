using EP.CMS.Domain.PageModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule
{
    public interface IPageExtRepository
    {
        string UpdatePage(Page page);
        string ReOrderNews(string[] newsIds);
    }
}
