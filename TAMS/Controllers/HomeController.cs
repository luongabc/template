
using System.Web.Mvc;
using TAMS;
using TAMS.DAL.ModelEntity;

namespace TAMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name,string password)
        {
            var userSession = UserContext.Search(name, password);
            if (userSession !=null)
            {
                Session.Add(Common.USER_SESSION, userSession);
                return RedirectToAction("Info");
            }
            return View();
        }
    }
}