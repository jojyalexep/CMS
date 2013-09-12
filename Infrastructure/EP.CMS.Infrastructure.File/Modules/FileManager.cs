using EP.CMS.Infrastructure.Files.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EP.CMS.Infrastructure.Files.Modules
{
    public class FileManager
    {


        public string ReadFile(string fileName, string path)
        {
            try
            {
                return Encoding.UTF8.GetString(ReadFileByte(fileName, path));
            }
            catch (Exception ex)
            {

                return "";
            }

        }

        public byte[] ReadFileByte(string fileName, string path)
        {
            return FileIOModule.ReadFile(path, fileName);
        }

        public void WriteFile(string fileName, string path, string content)
        {

            Byte[] byteContent = new UTF8Encoding(true).GetBytes(content);
            WriteFileByte(fileName, path, byteContent);
        }
        public void WriteFileByte(string fileName, string path, Byte[] content)
        {
            FileIOModule.WriteFile(path, fileName, content);
        }

        public string GetFileName(string prefix, int id, string extension)
        {
            return GetFileName(prefix + id, extension);
        }

        public string GetFileName(string fileName, string extension)
        {
            return fileName + "." + extension;
        }

        public static string GetFullPath(string path, string fileName)
        {
            return Path.Combine(path, fileName);
        }

        public string GetFullPath(string path, string fileName, string extension)
        {
            return Path.Combine(path, GetFileName(fileName, extension));
        }


        public string GetPhysicalPath(string relativePath)
        {
            if (HttpRuntime.AppDomainId == null)
                throw new Exception("No Http App Found");
            return Path.Combine(HttpRuntime.AppDomainAppPath, ConfigurationManager.AppSettings["contentdirectory"], relativePath);
        }

        public string GetHttpPath(string relativePath)
        {
            if (HttpContext.Current == null)
                throw new Exception("No Http Runtime Found");

            var requestUrl = HttpContext.Current.Request.Url;
            return Path.Combine(ConfigurationManager.AppSettings["contentdirectory"], relativePath);
        }
    }


    public enum PathLocationEnum : int
    {
        Default = 0,
        HttpLocation = 1,
        PhysicalLocation = 2

    }
}
