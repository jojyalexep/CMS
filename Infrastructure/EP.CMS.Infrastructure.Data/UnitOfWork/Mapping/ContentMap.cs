using EP.CMS.Domain.ContentModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{

    public class ContentMap : ClassMap<Content>
    {
        public ContentMap()
        {
            Table("CONTENT");
            Id(x => x.Id).Not.Nullable().GeneratedBy.Native(builder => builder.AddParam("sequence", "SEQ_CONTENT"));
            Map(x => x.Name);
            Map(x => x.DisplayName);
            Map(x => x.ContentLength);
            Map(x => x.Extension);
            Map(x => x.Url);
            Map(x => x.DirectoryId);
            Map(x => x.StatusId);
            Map(x => x.LanguageId);
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.PublishedDate);
            Map(x => x.IsDeleted);
            Map(x => x.TypeId);
            
        }
    }
}
