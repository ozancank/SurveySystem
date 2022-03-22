using System;
using System.Collections.Generic;

#nullable disable

namespace SurveySystem.Models
{
    public partial class AnswerLine
    {
        public int Id { get; set; }
        public int? AnswerId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Image { get; set; }
    }
}
