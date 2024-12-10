using EmployeeDomain.Dto;
using EmployeeDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpGet("GetDesignationById")]
        public async Task<ActionResult<GetDesignationDto>> GetDesignationById(int DesignationId)
        {
            if (DesignationId != 0)
            {
                return Ok(await _designationService.GetDesignationById(DesignationId));
            }
            return BadRequest();
        }

        [HttpGet("GetAllDesignationData")]
        public async Task<ActionResult<List<GetDesignationDto>>> GetAllDesignationData()
        {
            var response = await _designationService.GetAllDesignationData();
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        [HttpPost("AddDesignation")]
        public async Task<IActionResult> AddDesignation([FromBody] AddDesignationDto designation)
        {
            var response = await _designationService.AddDesignation(designation);
            if (response == true)
            {
                return Created();
            }
            return BadRequest();
        }
    }
}
