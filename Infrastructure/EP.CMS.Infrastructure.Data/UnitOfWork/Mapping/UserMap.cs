using EP.CMS.Domain.UserModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{

    public class UserMap : ClassMap<EPUser>
    {
        public UserMap()
        {
            Table("EPUSER");
            Id(x => x.Id).Not.Nullable().GeneratedBy.Native(builder => builder.AddParam("sequence", "SEQ_USER"));
            Map(x => x.Name);
            Map(x => x.Password);
            Map(x => x.Role);
            Map(x => x.Username);
            Map(x => x.IsDeleted);
        }

    }
}
