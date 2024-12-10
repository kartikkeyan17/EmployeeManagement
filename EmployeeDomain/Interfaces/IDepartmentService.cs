using EmployeeDomain.Dto;

namespace EmployeeDomain.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> AddDepartment(AddDepartmentDto addDepartmentDto);

        Task<List<GetDepartmentDto>> GetDepartmentData();
    }
}
