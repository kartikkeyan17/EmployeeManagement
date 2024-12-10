using EmployeeDomain.Dto;
using EmployeeDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult<GetDepartmentDto>> GetAllDepartment()
        {
            var response = await _departmentService.GetDepartmentData();
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDto addDepartmentDto)
        {
            var response = await _departmentService.AddDepartment(addDepartmentDto);
            if (response == true)
            {
                return Created();
            }
            return BadRequest();
        }
    }
}
