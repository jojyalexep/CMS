using EP.CMS.Domain.MenuModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{
    public class MenuMap : ClassMap<Menu>
    {
        public MenuMap()
        {
            Table("MENU");
            Id(x => x.Id).Not.Nullable().GeneratedBy.Native(builder => builder.AddParam("sequence", "SEQ_MENU"));
            Map(x => x.Name);
            Map(x => x.DisplayName);
            Map(x => x.ParentId);
            
            Map(x => x.IsExternalRef);
            Map(x => x.LinkedUrl);

            Map(x => x.PageType);
            Map(x => x.RefId);
            Map(x => x.Ranking);
            Map(x => x.StatusId);

            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);

            Map(x => x.LinkedId);
            Map(x => x.LanguageId);
            Map(x => x.IsDeleted);
            Map(x => x.IsLeaf);

            HasMany(x => x.ChildMenu).Cascade.All().KeyColumn("ParentId");


        }

    }
}
