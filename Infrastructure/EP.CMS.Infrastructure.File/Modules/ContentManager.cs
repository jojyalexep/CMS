using EP.CMS.Infrastructure.Files.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Files.Modules
{
    public class ContentManager : FileManager
    {
        private static string _prefix = "content";
        public byte[] GetContent(int id, string extension, int fileTypeId)
        {
            string relativePath = GetRelativePath(GetFileType(fileTypeId));
            return ReadFileByte(GetFileName(_prefix, id, extension), GetPhysicalPath(relativePath));
        }


        public string GetHttpPath(int id, string extension, int fileTypeId)
        {
            return GetHttpPath(GetRelativePath(GetFileType(fileTypeId)) + "\\" + GetFileName(_prefix, id, extension));
        }

        public void SaveContent(int id, string extension, byte[] content, int fileTypeId)
        {

            FileTypeEnum fileType = GetFileType(fileTypeId);
            string relativePath = GetRelativePath(fileType);

            if (fileType == FileTypeEnum.Image || fileType == FileTypeEnum.ThumbImage)
            {
                Image image = Image.FromStream(new MemoryStream(content));
                var imgFilePath = GetFullPath(GetPhysicalPath(relativePath), GetFileName(_prefix, id, extension));
                var imgThumbFilePath = GetFullPath(GetPhysicalPath(relativePath + "\\thumb"), GetFileName(_prefix, id, extension));
                var thumb = image.GetThumbnailImage(130, 77, null, new IntPtr());
                image.Save(imgFilePath);
                thumb.Save(imgThumbFilePath);
            }
            else 
            {
                WriteFileByte(GetFileName(_prefix, id, extension), GetPhysicalPath(relativePath), content);
            }
        }

        private string GetRelativePath(FileTypeEnum fileType)
        {

            string relativePath = "content\\";
            switch (fileType)
            {
                case FileTypeEnum.Image:
                    {
                        relativePath += "images";
                        break;
                    }

                case FileTypeEnum.ThumbImage:
                    {
                        relativePath += "images\\thumb\\";
                        break;
                    }

                case FileTypeEnum.Docs:
                    {
                        relativePath += "docs";
                        break;
                    }

                case FileTypeEnum.Audios:
                    {
                        relativePath += "audio";
                        break;
                    }

                case FileTypeEnum.Videos:
                    {
                        relativePath += "video";
                        break;
                    }
               

            }
            return relativePath;
        }

        private FileTypeEnum GetFileType(int fileTypeId)
        {
            return (FileTypeEnum)fileTypeId;
        }

    }
}
