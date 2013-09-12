using EP.CMS.Domain.PageModule;
using EP.CMS.Domain.PageModule.Entities;
using EP.CMS.Infrastructure.Data.SeedWork;
using EP.CMS.Infrastructure.Data.UnitOfWork;
using EP.CMS.Infrastructure.Files.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.Repositories
{
    public class PageRepository : Repository<Page>, IPageRepository
    {
        public PageRepository()
        {

        }
        public override Page Get(int id)
        {
            
            var page = base.Get(id);
            if (page != null)
            {
                page.Content = new PageManager().GetContent(id);
            }
            return page;
        }

        public override Page Add(Page item)
        {
            Page newPage = new Page();
            newPage = base.Add(item);
            if (!String.IsNullOrEmpty(item.Content))
                new PageManager().SaveContent(newPage.Id, item.Content);
            newPage.Content = item.Content;
            return newPage;
        }


        public override void Modify(Page item)
        {
            base.Modify(item);
            if (!String.IsNullOrEmpty(item.Content))
                new PageManager().SaveContent(item.Id, item.Content);
        }

    }

    
}
