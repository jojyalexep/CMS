using EP.CMS.Domain.ContentModule.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Data.UnitOfWork.Mapping
{


    public class DocsInfoMap : SubclassMap<Document>
    {
        public DocsInfoMap()
        {
            Table("DOCS");
            KeyColumn("Id");
        }
    }
}
