using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Models;
using SurveySystem.Utils;
using System;
using System.Linq;

namespace SurveySystem.Controllers
{
    public class QuestionController : BaseController
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            var model = db.Questions.ToList();
            return View(model);
        }

        public IActionResult Create(Question question)
        {
            if (question.QuestionLine != null)
            {
                question.CreateDate = DateTime.Now;
                question.CreateBy = NameSurname;
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id <= 0)
            {
                return NotFound();
            }
            var model = db.Questions.Find(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Question question)
        {
            db.Entry(question).State = EntityState.Modified;
            db.Entry(question).Property(e => e.CreateBy).IsModified = false;
            db.Entry(question).Property(e => e.CreateDate).IsModified = false;
            question.ModifyBy = NameSurname;
            question.ModifyDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id <= 0)
            {
                return NotFound();
            }
            var question = db.Questions.Find(Id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
