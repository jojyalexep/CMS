using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Infrastructure.ExternalServices.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.ExternalServices.Managers
{
    public class BinaryManager : IBinaryExtRepository
    {
        RepoService.English.CMSRepositorySEIClient englishService;
        RepoService.Arabic.CMSRepositorySEIClient arabicService;
        public BinaryManager()
        {

        }


        public string UpdateContent(Content content)
        {
            englishService = new RepoService.English.CMSRepositorySEIClient();
            arabicService = new RepoService.Arabic.CMSRepositorySEIClient();

            string response = String.Empty;


            string category = String.Empty;

            switch (content.TypeId)
            {
                case ((int)ContentTypeEnum.Image):
                    {
                        category = ImageCategoryUtil.GetCategoryName((content as ImageContent).CategoryId);
                        break;
                    }

                case ((int)ContentTypeEnum.Docs):
                    {
                        category = "docs";
                        break;
                    }

                case ((int)ContentTypeEnum.Audios):
                    {
                        category = "audios";
                        break;
                    }

                case ((int)ContentTypeEnum.Videos):
                    {
                        category = "videos";
                        break;
                    }
            }



            if (content.LanguageId == (int)Language.English)
            {
                var englishBinary = new RepoService.English.binaryContentModel();
                englishBinary.category = category;
                englishBinary.contentID = "content" + content.Id.ToString();
                englishBinary.displayName = content.DisplayName;
                englishBinary.contentType = content.Extension;
                englishBinary.contentBinary = content.Data;
                englishBinary.status = StatusUtil.GetStatusString(content.StatusId, content.IsDeleted);
                response = englishService.opn_updateBinaryContent("", englishBinary);
            }
            else if (content.LanguageId == (int)Language.Arabic)
            {
                var arabicBinary = new RepoService.Arabic.binaryContentModel();
                arabicBinary.category = category;
                arabicBinary.contentID = "content" + content.Id.ToString();
                arabicBinary.displayName = content.DisplayName;
                arabicBinary.contentType = content.Extension;
                arabicBinary.contentBinary = content.Data;
                arabicBinary.status = StatusUtil.GetStatusString(content.StatusId, content.IsDeleted);
                response = arabicService.opn_updateBinaryContent("", arabicBinary);
            }
            else
            {
                var englishBinary = new RepoService.English.binaryContentModel();
                englishBinary.category = category;
                englishBinary.contentID = "content" + content.Id.ToString();
                englishBinary.displayName = content.DisplayName;
                englishBinary.contentType = content.Extension;
                englishBinary.contentBinary = content.Data;
                englishBinary.status = StatusUtil.GetStatusString(content.StatusId, content.IsDeleted);
                response = englishService.opn_updateBinaryContent("", englishBinary);

                var arabicBinary = new RepoService.Arabic.binaryContentModel();
                arabicBinary.category = category;
                arabicBinary.contentID = "content" + content.Id.ToString();
                arabicBinary.displayName = content.DisplayName;
                arabicBinary.contentType = content.Extension;
                arabicBinary.contentBinary = content.Data;
                arabicBinary.status = StatusUtil.GetStatusString(content.StatusId, content.IsDeleted);
                response += arabicService.opn_updateBinaryContent("", arabicBinary);
            }
            WSReponseHandler.Handle(response);
            return response;
        }
    }
}
