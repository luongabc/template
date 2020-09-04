
using System.Web.Mvc;
using System.Web.Routing;
using TAMS.Entity;

namespace TAMS.Areas.Admin.Controllers
{
    public class SessionController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (User)Session[Common.USER_SESSION];
            if (session == null||
                session.isAdmin!=true)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Home", Action = "LogIn" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}