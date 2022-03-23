using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveySystem.Models;

namespace SurveySystem.Utils
{
    public class BaseController : Controller
    {
        public readonly SurveyContext db = new();
        public string UserCode { get; set; }
        public string NameSurname { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controllerName = context.Controller.ToString().Split(".")[2].Replace("Controller", "");
            if (HttpContext.Session.GetString("Code") == null)
            {
                context.Result = new RedirectResult("/Login/SignIn");
            }
            else if (HttpContext.Session.GetString("Admin") == null && (controllerName == "Question" || controllerName=="Person"))
            {
                context.Result = new RedirectResult("/Login/LogOut");
            }
            else
            {
                UserCode = HttpContext.Session.GetString("Code").ToString();
                NameSurname = HttpContext.Session.GetString("NameSurname").ToString();
            }
            base.OnActionExecuting(context);
        }
    }
}