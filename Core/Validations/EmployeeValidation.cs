using FluentValidation;
using Core.DTOs;
using System;

namespace Core.Validations
{ 
    public class EmployeeValidation : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidation()
        {

            RuleFor(x => x.Name).NotNull().WithMessage("Name is Required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Name).Length(0, 150).WithMessage("Name lenght 0 to 150 caracters");

            RuleFor(x => x.LasName).NotNull().WithMessage("LasName is Required");
            RuleFor(x => x.LasName).NotEmpty().WithMessage("LasName is Required");
            RuleFor(x => x.LasName).Length(0, 150).WithMessage("LasName lenght 0 to 150 caracters");

            RuleFor(x => x.Address).NotNull().WithMessage("Address is Required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is Required");
            RuleFor(x => x.Address).Length(0, 500).WithMessage("Address lenght 0 to 500 caracters");

            RuleFor(x => x.Note).Length(0, 600).WithMessage("Note lenght 0 to 600 caracters");

            RuleFor(x => x.YearOfbirth).NotNull().WithMessage("YearOfbirth is Required");
            RuleFor(x => x.YearOfbirth).NotEmpty().WithMessage("YearOfbirth is Required");
            RuleFor(x => x.YearOfbirth).ExclusiveBetween(1900, DateTime.Now.Year).WithMessage($"YearOfbirth would be Between 1900 and {DateTime.Now.Year}");

            RuleFor(x => x.MonthOfbirth).NotNull().WithMessage("MonthOfbirth is Required");
            RuleFor(x => x.MonthOfbirth).NotEmpty().WithMessage("MonthOfbirth is Required");
            RuleFor(x => x.MonthOfbirth).ExclusiveBetween(1, 12).WithMessage("MonthOfbirth would be Between 1 and 12");

            RuleFor(x => x.DayOfbirth).NotNull().WithMessage("DayOfbirth is Required");
            RuleFor(x => x.DayOfbirth).NotEmpty().WithMessage("DayOfbirth is Required");
            RuleFor(x => x.DayOfbirth).ExclusiveBetween(1, 31).WithMessage("DayOfbirth would be Between 1 and 31");

        }
    }
}