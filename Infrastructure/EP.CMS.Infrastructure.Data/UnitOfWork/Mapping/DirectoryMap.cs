using EP.CMS.Domain.DirectoryModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{
    public class DirectoryMap : ClassMap<Directory>
    {
        public DirectoryMap()
        {
            Table("DIRECTORY");
            Id(x => x.Id).Not.Nullable().GeneratedBy.Native(builder => builder.AddParam("sequence", "SEQ_DIRECTORY"));
            Map(x => x.Name);
            Map(x => x.ParentId);
            Map(x => x.CreatedDate);
            Map(x => x.CreatedBy);
            Map(x => x.IsDeleted);
            Map(x => x.CanModify);

            HasMany(x => x.ChildDirectory).Cascade.All().KeyColumn("ParentId");
        }
    }
}
