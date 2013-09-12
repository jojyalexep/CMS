using EP.CMS.Domain.PageModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{
    public class InfoMap : SubclassMap<Info>
    {
        public InfoMap()
        {
            Table("INFO");
            KeyColumn("Id");
            Map(x => x.Summary);
            References(c => c.Banner);
        }
    }
}
