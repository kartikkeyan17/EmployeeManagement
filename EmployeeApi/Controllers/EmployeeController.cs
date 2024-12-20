﻿using EmployeeDomain.Dto;
using EmployeeDomain.Interfaces;
using EmployeeDomain.Utilties.Validation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly EmployeeValidator _validationRules;
        public EmployeeController(IEmployeeService employeeService, EmployeeValidator validationRules)
        {
            _employeeService = employeeService;
            _validationRules = validationRules;
        }

        [HttpGet("GetAllEmployeeData")]
        public async Task<ActionResult<List<GetEmployeeDto>>> GetAllEmployeeData()
        {
            var response = await _employeeService.GetAllEmployeeData();
            if (response != null)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpGet("GetEmployeeByName/{EmployeeName}")]
        public async Task<ActionResult<GetEmployeeDto>> GetEmployeeDataByName(string EmployeeName)
        {
            if (!string.IsNullOrEmpty(EmployeeName))
            {
                var response = await _employeeService.GetEmployeeDataByName(EmployeeName);
                if (response != null)
                {
                    return Ok(response);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            var validModel = await _validationRules.ValidateAsync(addEmployeeDto);
            if (validModel.IsValid)
            {
                var response = await _employeeService.AddEmployee(addEmployeeDto);
                if (response == true)
                {
                    return Created();
                }
            }
            List<string> errors = validModel.Errors
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

            return BadRequest(errors);
        }

        [HttpPut("UpdateEmployee/{EmployeeId}")]
        public async Task<IActionResult> UpdateEmployee(int EmployeeId, [FromBody] AddEmployeeDto addEmployeeDto)
        {
            if (EmployeeId != 0)
            {
                return Ok(await _employeeService.UpdateEmployee(EmployeeId, addEmployeeDto));
            }
            return BadRequest();

        }

        [HttpDelete("DeleteEmployee/{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployee(int EmployeeId)
        {
            if (EmployeeId != 0)
            {
                return Ok(_employeeService.DeleteEmployee(EmployeeId));
            }
            return BadRequest();
        }
    }
}
