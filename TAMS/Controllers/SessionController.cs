
using System.Web.Mvc;
using System.Web.Routing;
using TAMS.Entity.Models;

namespace TAMS.Controllers
{
    public class SessionController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (User)Session[Common.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Home", Action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}