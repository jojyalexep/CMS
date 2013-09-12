using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Infrastructure.Files.Entities;
using EP.CMS.Infrastructure.Files.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Domain.ContentModule
{
    public static class ContentFactory
    {
        public static Content CreateNewContent(ContentTypeEnum contentType)
        {
            switch (contentType)
            {
                case ContentTypeEnum.Image:
                    {
                        ImageContent image = new ImageContent
                        {
                            Id = 0,
                            Extension = "jpg",
                            CreatedDate = DateTime.Now,
                            StatusId = (int)Status.Draft,

                        };

                        return image;
                    }

                default:
                    {
                        return new Content();
                    }
            }
        }

        public static Content CreateNewContent(byte[] file, string extension, int contentLength, string mimeType, int directory, string displayName)
        {
            if (mimeType.ToLower().Contains("image"))
            {
                ImageContent image = new ImageContent
                         {
                             Id = 0,
                             Data = file,
                             Extension = extension,
                             ContentLength = contentLength,
                             DirectoryId = directory,
                             DisplayName = displayName,
                             CreatedDate = DateTime.Now,
                             StatusId = (int)Status.Draft,
                             TypeId = (int)ContentTypeEnum.Image

                         };
                return image;
            }

            else if (mimeType.ToLower().Contains("pdf") ||
                     mimeType.ToLower().Contains("msword") ||
                     mimeType.ToLower().Contains("document"))
            {
                Document doc = new Document
                {
                    Id = 0,
                    Data = file,
                    Extension = extension,
                    ContentLength = contentLength,
                    DirectoryId = directory,
                    DisplayName = displayName,
                    CreatedDate = DateTime.Now,
                    StatusId = (int)Status.Draft,
                    TypeId = (int)ContentTypeEnum.Docs

                };
                return doc;
            }



            return new Content();
        }

        public static string CreateHttpUrl(Content content)
        {
            return new ContentManager().GetHttpPath(content.Id, content.Extension, content.TypeId);
        }


        public static FileTypeEnum GetFileType(int typeId)
        {

            switch (typeId)
            {
                case ((int)ContentTypeEnum.Image):
                    {
                        return FileTypeEnum.Image;
                    }
                case ((int)ContentTypeEnum.Docs):
                    {
                        return FileTypeEnum.Docs;
                    }
                case ((int)ContentTypeEnum.Audios):
                    {
                        return FileTypeEnum.Audios;
                    }
                case ((int)ContentTypeEnum.Videos):
                    {
                        return FileTypeEnum.Videos;
                    }
            }
            return FileTypeEnum.Image;


        }
    }


}
