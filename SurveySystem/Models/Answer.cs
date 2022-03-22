using System;
using System.Collections.Generic;

#nullable disable

namespace SurveySystem.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public string PersonCode { get; set; }
        public string PersonName { get; set; }
        public string UserCode { get; set; }
        public string Score { get; set; }
        public bool? IsComplete { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
}
