using EP.CMS.Infrastructure.Files.Entities;
using EP.CMS.Infrastructure.Files.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.ContentModule.Entities
{
    public class Content : Entity
    {
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual int ContentLength { get; set; }
        public virtual string Extension { get; set; }
        public virtual string Url
        {
            get
            {
                return ContentFactory.CreateHttpUrl(this);
            }
            set { }
        }
        public virtual int DirectoryId { get; set; }
        public virtual int LanguageId { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int TypeId { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime PublishedDate { get; set; }


       

    }
}
