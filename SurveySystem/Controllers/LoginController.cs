using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Models;
using System.Linq;

namespace SurveySystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly SurveyContext _db = new();

        public IActionResult SignIn(string Code, string Password)
        {
            if (Code == null)
            {
                return View();
            }
            var person = _db.Persons.FirstOrDefault(m => m.Code == Code && m.Password == Password);
            if (person != null)
            {
                HttpContext.Session.SetString("Code", person.Code);
                HttpContext.Session.SetString("NameSurname", person.NameSurname);
                return RedirectToAction("Index", "Answer");
            }
            else
            {
                ViewBag.Error = "Kullanıcı Adı veya Şifre Hatalı";
                return View();
            }
        }
    }
}