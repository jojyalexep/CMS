using EP.CMS.Domain.PageModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{
    public class PageMap : ClassMap<Page>
    {
        public PageMap()
        {
            Table("PAGE");
            Id(x => x.Id).Not.Nullable().GeneratedBy.Native(builder => builder.AddParam("sequence", "SEQ_PAGE"));
            Map(x => x.Name);
            Map(x => x.Keywords);
            Map(x => x.Title);
            Map(x => x.LanguageId);
            Map(x => x.StatusId);
            Map(x => x.TypeId);
            Map(x => x.LinkedId);
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.PublishedDate);
            Map(x => x.IsDeleted);
        }

    }
}
