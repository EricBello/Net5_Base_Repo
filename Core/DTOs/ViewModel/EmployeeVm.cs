using System;

namespace Core.DTOs.ViewModel
{
    public partial class EmployeeVm : EmployeeDto
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

    }
}