using EP.CMS.Domain.CommonModule.Enum;
using EP.CMS.Domain.ContentModule.Entities;
using EP.CMS.Domain.DirectoryModule;
using EP.CMS.Domain.PageModule;
using EP.CMS.Presentation.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EP.CMS.Presentation.Web.Controllers
{
    [Authorize]
    [HandleErrorWithELMAH]
    public class MediaController : Controller
    {
        //
        // GET: /Images/


        public IContentServices _contentServices;
        public IDirectoryServices _directoryServices;


        public MediaController(IContentServices contentServices, IDirectoryServices directoryServices)
        {
            _contentServices = contentServices;
            _directoryServices = directoryServices;
        }

        public ActionResult New(string directory = "images")
        {
            ViewBag.Directory = directory;
            return View();
        }

        [HttpPost]
        public ActionResult New(string Directory, HttpPostedFileBase File, string DisplayName, string Category)
        {
            if (File != null)
            {
                MemoryStream target = new MemoryStream();
                File.InputStream.CopyTo(target);
                byte[] data = target.ToArray();


                var dir = _directoryServices.GetDirectory(Directory);
                var content = _contentServices.Create(data, Path.GetExtension(File.FileName).Replace(".", ""), File.ContentLength, File.ContentType, dir.Id, DisplayName);
                content.CreatedDate = DateTime.Now;
                content.CreatedBy = User.Identity.Name;
                content = _contentServices.Add(content);
                return Json(new { Success = true, DisplayName = content.DisplayName, Id = content.Id }, "application/json", System.Text.Encoding.UTF8,
                        JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = false });
        }

        public ActionResult Edit(int imageId, string directory = "root")
        {
            var image = _contentServices.GetContent(imageId);
            ViewBag.Directory = _directoryServices.GetDirectory(image.DirectoryId).Name;
            return View(image);
        }


        public ActionResult UpdateImage(int oldId, int newId)
        {
            var oldImage = _contentServices.GetContent(oldId);
            var newImage = _contentServices.GetContent(newId);
            oldImage.Data = newImage.Data;
            oldImage.Url = newImage.Url;
            oldImage.CreatedDate = DateTime.Now;
            oldImage.CreatedBy = User.Identity.Name;
            _contentServices.Edit(oldImage);
            return Json(new { Success = true });
        }

        public ActionResult Index(string directory = "images")
        {
            ViewBag.Directory = directory;
            return View();
        }

        public ActionResult Banner(string directory = "home")
        {
            ViewBag.Directory = directory;
            return View();
        }


        public ActionResult Popup(string directory = "images")
        {
            ViewBag.Directory = directory;
            return View();
        }

        public ActionResult MenuNew(string directory = "images")
        {
            ViewBag.Directory = directory;
            return View();
        }

        public ActionResult Upload()
        {
            var imageList = _contentServices.GetAll().Cast<ImageContent>();
            return View(imageList);
        }

        public ActionResult GetDirectoryContent(int directoryId)
        {
            var contentList = _contentServices.GetDirectoryContents(directoryId);
            return PartialView("_Content", contentList);
        }


        public ActionResult BannerContent(string directory)
        {
            var contents = _contentServices.GetAllImagesOfCategory(ImageCategoryEnum.Banner).Where(p => p.DisplayName.StartsWith(directory));
            var englishContent = contents.Where(p => p.LanguageId == (int)Language.English);
            ViewBag.ArabicModel = contents.Where(p => p.LanguageId == (int)Language.Arabic);
            return PartialView(englishContent);
        }


        public ActionResult GetAllDocuments()
        {
            var contents = _contentServices.GetAllOfType(ContentTypeEnum.Docs);
            List<EditorContent> contentList = new List<EditorContent>();
            foreach (var content in contents)
            {
                contentList.Add(new EditorContent
                {
                    title = content.DisplayName,
                    value = ConfigurationManager.AppSettings["docs-base-url"] + "content" + content.Id
                });
            }
            return Json(contentList, JsonRequestBehavior.AllowGet);
        }


         public ActionResult GetAllImages()
        {
            var contents = _contentServices.GetAllOfType(ContentTypeEnum.Image);
            List<EditorContent> contentList = new List<EditorContent>();
            foreach (var content in contents)
            {
                contentList.Add(new EditorContent
                {
                    title = content.DisplayName,
                    value = ConfigurationManager.AppSettings["img-base-url"] + "content" + content.Id
                    //value = content.Url
                });
            }
            return Json(contentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int[] idList)
        {
            foreach (var id in idList)
            {
                _contentServices.Delete(id);
            }

            return Json(new { Success = true });
        }




    }

    public class EditorContent
    {
        public string title { get; set; }
        public string value { get; set; }
    }
}
