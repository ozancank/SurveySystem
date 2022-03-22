using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveySystem.Models;

namespace SurveySystem.Utils
{
    public class BaseController : Controller
    {
        public readonly SurveyContext db = new();
        public string Code { get; set; }
        public string NameSurname { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("Code") == null)
            {
                context.Result = new RedirectResult("/Login/SignIn");
            }
            else
            {
                Code = HttpContext.Session.GetString("Code").ToString();
                NameSurname = HttpContext.Session.GetString("NameSurname").ToString();
            }
            base.OnActionExecuting(context);
        }
    }
}