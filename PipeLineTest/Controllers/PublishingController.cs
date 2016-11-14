using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebGrease.Css;

namespace PipeLineTest.Controllers
{
    public class PublishingController : Controller
    {
        // GET: Publishing
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                   string path = Path.Combine(Server.MapPath("~/Files"),
                      Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    return Json(new
                    {
                        Message = "File Uploaded Successfully!", FilePath = file.FileName
                    }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { Error = false, Message = "File Not Uploaded" }, JsonRequestBehavior.AllowGet);
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }

        public FileResult ShowDocument(string FilePath)
        {
            return File(Server.MapPath("~/Files/") + FilePath, GetMimeType(FilePath));
        }

        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }

    }
}
