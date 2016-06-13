using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUI_eksamen_web.Controllers
{
    public class JokeController : Controller
    {
        // GET: NewJoke
        public ActionResult NewJoke()
        {
            return View();
        }

        // GET: SearchJoke
        public ActionResult SearchJoke()
        {
            return View();
        }
    }
}