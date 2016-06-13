using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GUI_eksamen_web.Models;

namespace GUI_eksamen_web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: NewJoke
        public ActionResult NewJoke()
        {
            return View();
        }

        // GET: SearchJoke
        public ActionResult SearchJokes(string searchString)
        {
        
            JokeDbContext db = new JokeDbContext();
            return View(db.Jokes.ToList());
        }
    }
}