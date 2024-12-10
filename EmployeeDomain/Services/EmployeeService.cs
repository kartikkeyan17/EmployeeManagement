using EmployeeDomain.Dto;
using EmployeeDomain.Interfaces;
using EmployeeDomain.ViewModels;
using EmployeeInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDomain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;

        }
        public async Task<bool> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            bool isSucess = false;
            if (addEmployeeDto != null)
            {
                Employee employeeData = new Employee()
                {

                    EmployeeName = addEmployeeDto.EmployeeName,
                    DepartmentId = addEmployeeDto.DepartmentId,
                    DesignationId = addEmployeeDto.DesignationnId,
                    Email = addEmployeeDto.Email,
                    Salary = addEmployeeDto.Salary,
                    Active = true,
                    DeleteFlag = false
                };

                await _employeeContext.Employees.AddAsync(employeeData);
                int id = await _employeeContext.SaveChangesAsync();

                isSucess = id == 1;
            }
            return isSucess;

        }

        public async Task<List<GetEmployeeDto>> GetAllEmployeeData()
        {

            List<GetEmployeeDto> employeeData = await _employeeContext.Employees.AsNoTracking()
                                                                       .Where(w => w.Active == true && w.DeleteFlag == false)
                                                                       .Select(s => new GetEmployeeDto
                                                                       {
                                                                           EmployeeName = s.EmployeeName,
                                                                           DesignationnName = s.Designation.Designation,
                                                                           DepartmentName = s.Department.Department,
                                                                           Email = s.Email,
                                                                           Salary = s.Salary ?? 0
                                                                       }).ToListAsync() ?? new List<GetEmployeeDto>();

            return employeeData;
        }

        public async Task<GetEmployeeDto> GetEmployeeDataByName(string EmployeeName)
        {
            GetEmployeeDto employeeDatas = await _employeeContext.Employees.AsNoTracking()
                                                                       .Where(w => w.Active == true && w.DeleteFlag == false &&
                                                                       w.EmployeeName == EmployeeName)
                                                                       .Select(s => new GetEmployeeDto
                                                                       {
                                                                           EmployeeName = s.EmployeeName,
                                                                           DesignationnName = s.Designation.Designation,
                                                                           DepartmentName = s.Department.Department,
                                                                           Email = s.Email,
                                                                           Salary = s.Salary ?? 0
                                                                       }).FirstOrDefaultAsync() ?? new GetEmployeeDto();

            return employeeDatas;
        }

        public async Task<bool> DeleteEmployee(int EmployeeId)
        {
            bool isSucess = false;
            if (EmployeeId != 0)
            {
                Employee employeeData = _employeeContext.Employees.Where(w => w.Active == true && w.DeleteFlag == false && w.EmployeeId == EmployeeId)
                                                        .Select(s => new Employee
                                                        {
                                                            EmployeeId = s.EmployeeId
                                                        }).FirstOrDefault() ?? new Employee();

                if (employeeData.EmployeeId != 0)
                {
                    _employeeContext.Employees.Remove(employeeData);
                    int id = await _employeeContext.SaveChangesAsync();
                    isSucess = id == 1;
                }
            }
            return isSucess;
        }

        public async Task<bool> UpdateEmployee(int EmployeeId, AddEmployeeDto addEmployeeDto)
        {
            bool isSucess = false;
            if (EmployeeId != 0)
            {
                Employee employeeData = await _employeeContext.Employees.FirstOrDefaultAsync(w => w.Active == true && w.DeleteFlag == false && w.EmployeeId == EmployeeId) ?? new Employee();

                if (employeeData.EmployeeId != 0)
                {
                    employeeData.EmployeeName = addEmployeeDto.EmployeeName;
                    employeeData.DepartmentId = addEmployeeDto.DepartmentId;
                    employeeData.DesignationId = addEmployeeDto.DesignationnId;
                    employeeData.Email = addEmployeeDto.Email;
                    employeeData.Salary = addEmployeeDto.Salary;

                    _employeeContext.Employees.Update(employeeData);
                    int id = await _employeeContext.SaveChangesAsync();

                    isSucess = id == 1;
                }
            }
            return isSucess;
        }
    }
}
