using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Models;
using SurveySystem.Utils;
using System;
using System.Linq;

namespace SurveySystem.Controllers
{
    public class PersonController : BaseController
    {
        public IActionResult Index()
        {
            var model = db.Persons.ToList();
            return View(model);
        }

        public IActionResult Create(Person person)
        {
            if (person.NameSurname != null)
            {
                person.CreateDate = DateTime.Now;
                person.CreateBy = "System";
                db.Persons.Add(person);
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
            var model = db.Persons.Find(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            db.Entry(person).State = EntityState.Modified;
            db.Entry(person).Property(e => e.CreateBy).IsModified = false;
            db.Entry(person).Property(e => e.CreateDate).IsModified = false;
            person.ModifyBy = "System Edit";
            person.ModifyDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id <= 0)
            {
                return NotFound();
            }
            var person = db.Persons.Find(Id);
            db.Persons.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}