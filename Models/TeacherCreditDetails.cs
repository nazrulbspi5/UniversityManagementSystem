using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UoUWebApp.Models
{
    public class TeacherCreditDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeptId { get; set; }
        public float TotalCredits { get; set; }
        public float CreditTaken { get; set; }
    }
}