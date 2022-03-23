using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveySystem.Models;
using SurveySystem.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurveySystem.Controllers
{
    public class AnswerController : BaseController
    {
        public IActionResult Index()
        {
            var model = db.Answers.Where(m => m.UserCode == UserCode).ToList();
            return View(model);
        }

        public IActionResult Create(string Code)
        {
            if (Code == null)
            {
                List<SelectListItem> personList = (from person in db.Persons
                                                   where person.Code != UserCode
                                                   select new SelectListItem
                                                   {
                                                       Text = person.NameSurname,
                                                       Value = person.Code.ToString()
                                                   }).ToList();

                ViewBag.Person = new SelectList(personList.OrderBy(m => m.Text), "Value", "Text");

                var questionModel = db.Questions.ToList();
                return View(questionModel);
            }
            else
            {
                CalculateScore(Code);
                return RedirectToAction("Index");
            }
        }

        public void CalculateScore(string code)
        {
            double yes = 0, no = 0, result = 0;
            var answer = db.Answers.FirstOrDefault(m => m.PersonCode == code && m.UserCode == UserCode);
            var answerLine = db.AnswerLines.Where(m => m.AnswerId == answer.Id).ToList();
            foreach (var item in answerLine)
            {
                if (item.Answer == Constants.AnswerType.Yes)
                    yes++;
                else
                    no++;
            }

            result = (yes / (yes + no)) * 100;

            if (result >= 80)
                answer.IsComplete = true;
            else
                answer.IsComplete = false;

            answer.Score = result.ToString();
            db.SaveChanges();
        }


        public string SendData(AnswerModel answerModel)
        {
            int? month = DateTime.Now.Month;
            var model = db.Answers.FirstOrDefault(m => m.PersonCode == answerModel.Code && m.UserCode == UserCode && m.CreateDate.Value.Month == month);

            if (model != null)
            {
                SaveAnswerLine(answerModel.Question, answerModel.Answer, model.Id);
            }
            else
            {
                Answer answer = new()
                {
                    PersonCode = answerModel.Code,
                    PersonName = answerModel.NameSurname,
                    UserCode = UserCode,
                    CreateDate = DateTime.Now,
                    CreateBy = NameSurname
                };
                db.Answers.Add(answer);
                db.SaveChanges();
                SaveAnswerLine(answerModel.Question, answerModel.Answer, answer.Id);
            }
            return "True";

        }

        public void SaveAnswerLine(string question, string answer, int answerId)
        {
            var model = db.AnswerLines.FirstOrDefault(m => m.AnswerId == answerId && m.Question == question);

            if (model != null)
            {
                model.Answer = answer;
                db.SaveChanges();
            }
            else
            {
                AnswerLine answerLine = new()
                {
                    AnswerId = answerId,
                    Answer = answer,
                    Question = question
                };
                db.AnswerLines.Add(answerLine);
                db.SaveChanges();
            }
        }

        public IActionResult Detail(int? Id)
        {
            var model=db.AnswerLines.Where(m => m.AnswerId == Id).ToList();
            return View(model);
        }
    }
}
