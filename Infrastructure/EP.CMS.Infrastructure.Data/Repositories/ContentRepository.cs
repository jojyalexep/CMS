using EP.CMS.Domain.ContentModule;
using EP.CMS.Domain.ContentModule.Entities;
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
    public class ContentRepository : Repository<Content>, IContentRepository
    {
        public ContentRepository()
        {

        }
        public override Content Get(int id)
        {

            var content = base.Get(id);
            content.Data = new ContentManager().GetContent(id, content.Extension, content.TypeId);
            return content;
        }

        public override Content Add(Content item)
        {
            item = base.Add(item);
            new ContentManager().SaveContent(item.Id, item.Extension, item.Data, item.TypeId);
            return item;
        }
        public override void Modify(Content item)
        {
            base.Modify(item);
            new ContentManager().SaveContent(item.Id, item.Extension, item.Data, item.TypeId);
        }

        
    }
}
