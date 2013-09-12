using EP.CMS.Domain.DirectoryModule;
using EP.CMS.Domain.DirectoryModule.Entities;
using EP.CMS.Presentation.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EP.CMS.Presentation.Web.Controllers
{
    [Authorize]
    [HandleErrorWithELMAH]
    public class DirectoryController : Controller
    {
        //
        // GET: /Directory/

        public IDirectoryServices _directoryServices;


        public DirectoryController(IDirectoryServices directoryServices)
        {
            _directoryServices = directoryServices;
        }

        public ActionResult Index()
        {
            return View();
        }




        public ActionResult Edit(int dirId = 0)
        {
            GetParentDirectory();
            Directory directory = new Directory();
            if (dirId == 0)
            {

            }
            else
            {
                directory = _directoryServices.GetDirectory(dirId);
            }
            return View(directory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Directory directory)
        {

            var action = ToolbarActionManager.GetAction();
            if (directory.Id == 0)
            {
                _directoryServices.Add(directory);
            }
            else
            {
                directory = (Directory)_directoryServices.GetDirectory(directory.Id);
                UpdateModel(directory);
                _directoryServices.Edit(directory);
            }
            return Json(new { Success = true });
        }



        public ActionResult Rename(string id, string name)
        {
            if (_directoryServices.GetDirectory(name) == null)
            {
                var dir = _directoryServices.GetDirectory(id);
                if (dir.CanModify)
                {
                    dir.Name = name.ToLower();
                    _directoryServices.Edit(dir);
                    return Json(new { Success = true, Message = "Directory renamed" });
                }
                return Json(new { Success = false, Message = "Directory cannot be renamed!!!" });
            }
            return Json(new { Success = false, Message = "Directory name exists!!!" });
        }


        public ActionResult New(string id, string name)
        {
            if (_directoryServices.GetDirectory(name) == null)
            {
                var dir = _directoryServices.Create();
                dir.ParentId = _directoryServices.GetDirectory(id).Id;
                dir.Name = name.ToLower();
                dir.CanModify = true;
                _directoryServices.Add(dir);
                return Json(new { Success = true, Message = "Directory added" });
            }
            return Json(new { Success = false, Message = "Directory name exists!!!" });
        }


        public ActionResult Delete(string id)
        {
            var response = _directoryServices.Delete(id);
            if (response)
            {
                return Json(new { Success = true, Message = "Directory deleted" });
            }
            return Json(new { Success = false, Message = "Directory cannot be deleted!!!" });
        }

        public ActionResult Tree()
        {

            return View(_directoryServices.GetDirectory(1));
        }

        public ActionResult GetAll()
        {

            return View(_directoryServices.GetDirectory(1));
        }

        private void GetParentDirectory()
        {
            List<Directory> directoryList = new List<Directory>();
            GetDirectoryAsList(_directoryServices.GetDirectory(1), ref directoryList);
            ViewBag.ParentDirectory = new SelectList(directoryList, "Id", "Name");

        }


        private void GetDirectoryAsList(Directory directory, ref List<Directory> directoryList)
        {
            directoryList.Add(directory);
            if (directory.ChildDirectory != null)
            {
                foreach (var childMenu in directory.ChildDirectory)
                {
                    GetDirectoryAsList(childMenu, ref directoryList);
                }
            }
        }

    }
}
