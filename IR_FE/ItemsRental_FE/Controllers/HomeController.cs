using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ItemsRental_FE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /// <summary>
        /// This will send the user guide to the user
        /// </summary>
        public void GetUserGuide()
        {
            string FileName = Path.Combine(Server.MapPath("~/UsersGuide.docx"));
            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();

            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = \"{0}\"", Path.GetFileName(FileName)));
            response.TransmitFile(FileName);
            response.Flush();
            response.End();
        }

        public ActionResult Help()
        {
            return View();
        }
    }
}
