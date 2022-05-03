using Core.DTOs;
using Core.DTOs.ViewModel;
using Core.Interfaces.Services;
using Core.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sml.stars.catalog.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public ExampleController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<EmployeeVm>),200)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _employeeService.GetByIdAsync(id));
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<EmployeeVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _employeeService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<EmployeeVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] EmployeeDto obj)
        {
            return Ok(await _employeeService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<EmployeeVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] EmployeeDto obj)
        {
            return Ok(await _employeeService.UpdateAsync(id, obj));
        } 

    }
}
