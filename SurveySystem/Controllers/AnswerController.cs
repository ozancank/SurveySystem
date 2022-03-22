using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveySystem.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SurveySystem.Controllers
{
    public class AnswerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            List<SelectListItem> personList = (from person in db.Persons where person.Code != Code select new SelectListItem
            {
                Text=person.NameSurname,
                Value=person.Code.ToString()
            }).ToList();

            ViewBag.Person = new SelectList(personList.OrderBy(m => m.Text), "Value", "Text");

            var questionModel = db.Questions.ToList();
            return View(questionModel);
        }
    }
}
