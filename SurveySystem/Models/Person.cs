using System;
using System.Collections.Generic;

#nullable disable

namespace SurveySystem.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
