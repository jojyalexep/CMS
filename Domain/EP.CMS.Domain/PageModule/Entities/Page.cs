
using EP.CMS.Domain.CommonModule.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.PageModule.Entities
{
    public class Page : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual int LanguageId { get; set; }
        public virtual int StatusId { get; set; }
        public virtual int TypeId { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime PublishedDate { get; set; }
        public virtual int LinkedId { get; set; }

        public virtual PageTypeEnum PageType
        {
            get
            {
                if (this.GetType() == typeof(Info))
                {
                    return PageTypeEnum.Info;
                }

                if (this.GetType() == typeof(News))
                {
                    return PageTypeEnum.News;
                }
                return PageTypeEnum.All;
            }
        }


        public virtual bool IsLinked()
        {
            if (LinkedId != 0)
                return true;
            return false;
        }
    }

}
