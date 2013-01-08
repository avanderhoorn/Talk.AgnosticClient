using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Framework;

namespace Sample.Controllers
{
    public partial class SlideController : Controller
    {
        public virtual ActionResult Index(string index)
        {
            return View(index);
        }

        [HttpGet]
        public virtual ActionResult Json()
        {
            return Json(new { Name = "John", Age = 23, Type = "json" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult Jsonp()
        {
            return new JsonpResult(new { Name = "Peter", Age = 21, Type = "jsonp" });
        }

        [HttpGet]
        [HttpHeader("Access-Control-Allow-Origin", "*")]
        public virtual ActionResult Cors()
        {
            return Json(new { Name = "Colin", Age = 23, Type = "cors" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual ActionResult ContentType()
        { 
            return Json(new { Name = "Chris", Age = 50 }, "application/vnd.glimpse.person-v1+json", JsonRequestBehavior.AllowGet);
        }
    }
}
