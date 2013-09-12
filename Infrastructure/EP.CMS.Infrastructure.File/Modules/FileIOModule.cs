using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Files.Modules
{
    internal static class FileIOModule
    {
        public static byte[] ReadFile(string path, string fileName)
        {
            if (String.IsNullOrEmpty(path) || String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("Path/File is null");
            }

            if (!Directory.Exists(path))
            {
                throw new ArgumentNullException("Directory not found");
            }

            string filePath = Path.Combine(path, fileName);
            if (!File.Exists(filePath))
            {
                throw new IOException("File not found");
            }
            return File.ReadAllBytes(filePath);
        }

        public static void WriteFile(string path, string fileName, Byte[] content)
        {
            if (String.IsNullOrEmpty(path) || String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("Path/File is null");
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileInfo fileInfo = new FileInfo(Path.Combine(path, fileName));
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                using (FileStream fs = fileInfo.Create())
                {
                    //Add some information to the file.
                    fs.Write(content, 0, content.Length);
                }
            }
        }

        public static void DeleteFile(string path, string fileName, Byte[] content)
        {
            if (String.IsNullOrEmpty(path) || String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("Path/File is null");
            }
            if (!Directory.Exists(path))
            {
                throw new ArgumentNullException("Directory not found");
            }
            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                throw new ArgumentNullException("File not found");
            }


        }
    }
}
