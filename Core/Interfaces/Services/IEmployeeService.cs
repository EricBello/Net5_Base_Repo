using Core.DTOs;
using Core.DTOs.ViewModel;
using Core.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<Response<EmployeeVm>> InsertAsync(EmployeeDto dto);
        Task<Response<EmployeeVm>> UpdateAsync(int id, EmployeeDto dto);
        Task<Response<EmployeeVm>> GetByIdAsync(int id);
        Task<PagedResponse<IList<EmployeeVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    }
}
