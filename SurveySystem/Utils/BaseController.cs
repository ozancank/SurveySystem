using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveySystem.Models;

namespace SurveySystem.Utils
{
    public class BaseController : Controller
    {
        public readonly SurveyContext db = new();
        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            if (HttpContext.Session.TryGetValue("Code",out _) == null)
            {
                context.Result = new RedirectResult("/Login/SignIn");
            }
            else{

            }
            base.OnActionExecuting(context);
        }
    }
}
