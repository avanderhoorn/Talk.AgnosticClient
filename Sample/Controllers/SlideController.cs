using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Controllers
{
    public class SlideController : Controller
    {
        public ActionResult Index(string index)
        {
            return View(index);
        }
    }
}
