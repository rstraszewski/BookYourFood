using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookYourFood.Controllers
{
    public class SelectCreatorController : Controller
    {
        // GET: SelectCreator
        public ActionResult Index(long id)
       { 
            return View();
        }

        [HttpPost]
        public ActionResult Index(long id, string name)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}