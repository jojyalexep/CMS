using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.Files.Modules
{
    public class PageManager : FileManager
    {
        private static string _prefix = "page";
        private static string _relativePath = "pages";
        private static string _extension = "html";

        public string GetContent(int id)
        {
            return ReadFile(GetFileName(_prefix, id, _extension), GetPhysicalPath(_relativePath));
        }

        public void SaveContent(int id, string content)
        {
            WriteFile(GetFileName(_prefix, id, _extension), GetPhysicalPath(_relativePath), content);
        }
    }
}
