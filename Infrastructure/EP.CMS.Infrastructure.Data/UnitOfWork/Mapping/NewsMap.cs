using EP.CMS.Domain.PageModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{
    public class NewsMap : SubclassMap<News>
    {
        public NewsMap()
        {
            Table("NEWS");
            KeyColumn("Id");
            Map(x => x.Summary);
            Map(x => x.SubTitle);
            Map(x => x.Ranking);
            Map(x => x.WhatsNew);
            Map(x => x.NewsDate);
            References(c => c.NewsImage);
          

        }
    }
}
