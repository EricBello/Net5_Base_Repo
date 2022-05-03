using AutoMapper;
using Core.DTOs;
using Core.DTOs.ViewModel;
using Core.Entities;

namespace Core.Mapping
{
    public class BaseMappings : Profile
    {
        public BaseMappings()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeVm>().ReverseMap();
        }
    }
}
