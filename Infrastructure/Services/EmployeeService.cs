using AutoMapper;
using Core.DTOs;
using Core.DTOs.ViewModel;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Wrappers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryAsync<Employee> _employeeRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployeeDto> _validator;

        public EmployeeService(IRepositoryAsync<Employee> employeeRepo, IMapper mapper, IValidator<EmployeeDto> validator)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _employeeRepo.Exists(x => x.Id.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Employee not found");
        }

        public async Task<Response<EmployeeVm>> InsertAsync(EmployeeDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Employee>(dto);
            obj.CreateDate = DateTime.Now;

            return new Response<EmployeeVm>(_mapper.Map<EmployeeVm>(await _employeeRepo.AddAsync(obj)));

        }

        public async Task<Response<EmployeeVm>> UpdateAsync(int id, EmployeeDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _employeeRepo.WhereAsync(x => x.Id.Equals(id));
            var obj = _mapper.Map<Employee>(dto);
            obj.Id = id;
            obj.CreateDate = objDb.CreateDate;


            //Note: You can automap the object or map manualy, as this code down.

            //#region Mapping 
            //obj.Name = dto.Name;
            //obj.LasName = dto.LasName;
            //obj.Address = dto.Address;
            //obj.Status = dto.Status;
            //obj.Note = dto.Note;
            //obj.YearOfbirth = dto.YearOfbirth;
            //obj.MonthOfbirth = dto.MonthOfbirth;
            //obj.DayOfbirth = dto.DayOfbirth;
            //#endregion Mapping

            return new Response<EmployeeVm>(_mapper.Map<EmployeeVm>(await _employeeRepo.UpdateAsync(obj)));
        }

        public async Task<Response<EmployeeVm>> GetByIdAsync(int id)
        {
            var data = await _employeeRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Employee not found by id={id}");
            }

            return new Response<EmployeeVm>(_mapper.Map<EmployeeVm>(data));
        }



        public async Task<PagedResponse<IList<EmployeeVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<Employee, bool>>> queryFilter = new List<Expression<Func<Employee, bool>>>();

            if (filter != null || filter.Length > 0)
            {
                queryFilter.Add(x => x.Name.Contains(filter) || x.LasName.Contains(filter));
            }

            var list = await _employeeRepo.GetPagedList(pageNumber, pageSize, queryFilter);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Employee not found");
            }

            return new PagedResponse<IList<EmployeeVm>>(_mapper.Map<IList<EmployeeVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
