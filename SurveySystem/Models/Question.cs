using System;
using System.Collections.Generic;

#nullable disable

namespace SurveySystem.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public string QuestionLine { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
    }
}
