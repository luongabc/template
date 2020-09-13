
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMS;
using TAMS.Controllers;
using TAMS.DAL.ModelEntity;
using TAMS.Entity;
using TAMS.Entity.Models;
namespace TAMS.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        // GET: LoginAndRegister
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
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
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (UserContext.Check(user) > 0) return View();
            if (UserContext.Create(user) < 1) return View();
            Session.Add(Common.USER_SESSION, user);
            return RedirectToAction("Index", "InfoUser");
        }
    }
}