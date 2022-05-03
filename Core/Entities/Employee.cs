using System;

namespace Core.Entities
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LasName { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
        public int YearOfbirth { get; set; }
        public int DayOfbirth { get; set; }
        public int MonthOfbirth { get; set; }
        public DateTime CreateDate { get; set; }
    }
}