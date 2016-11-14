using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

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
                        Message = "File Uploaded Successfully!",
                        FilePath = file.FileName
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { Error = false, Message = "File Not Uploaded" }, JsonRequestBehavior.AllowGet);
                }

            ViewBag.Message = "You have not specified a file.";

            return View();
        }

        public FileResult ShowDocument(string filePath)
        {
            return File(Server.MapPath("~/Files/") + filePath, GetMimeType(filePath));
        }

        //Multipurpose Internet Mail Extensions(Mime) - used to describe content of various files
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
