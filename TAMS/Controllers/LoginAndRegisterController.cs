
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS;
using TAMS.DAL.ModelEntity;

namespace exam.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        // GET: LoginAndRegister
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string name,string password)
        {
            var userSession = UserContext.Search(name, password);
            if (userSession != null)
            {
                Session.Add(Common.USER_SESSION, userSession);
                return RedirectToAction("Index","InfoUser");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}