using EmployeeDomain.Dto;
using EmployeeDomain.Interfaces;
using EmployeeDomain.ViewModels;
using EmployeeInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDomain.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeContext _employeeContext;

        public DepartmentService(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;

        }
        public async Task<bool> AddDepartment(AddDepartmentDto addDepartmentDto)
        {
            bool isSucess = false;
            if (!string.IsNullOrEmpty(addDepartmentDto.DepartmentName))
            {
                DepartmentMaster departmentMaster = new DepartmentMaster()
                {
                    Department = addDepartmentDto.DepartmentName,
                    Active = true,
                    DeleteFlag = false
                };

                await _employeeContext.DepartmentMasters.AddAsync(departmentMaster);
                int id = await _employeeContext.SaveChangesAsync();

                isSucess = id == 1;
            }
            return isSucess;
        }

        public async Task<List<GetDepartmentDto>> GetDepartmentData()
        {
            List<GetDepartmentDto> result = new List<GetDepartmentDto>();

            List<Department> departmentDatas = await _employeeContext.DepartmentMasters.AsNoTracking()
                                                                       .Where(w => w.Active == true && w.DeleteFlag == false)
                                                                       .Select(s => new Department
                                                                       {
                                                                           DepartmentId = s.DepartmentId,
                                                                           DepartmentName = s.Department
                                                                       }).ToListAsync();

            if (departmentDatas.Any())
            {
                result = departmentDatas.Select(s => new GetDepartmentDto
                {
                    DepartmentId = s.DepartmentId,
                    DepartmentName = s.DepartmentName,
                }).ToList();
            }

            return result;

        }
    }
}
