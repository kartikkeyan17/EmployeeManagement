using EmployeeDomain.Dto;

namespace EmployeeDomain.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> AddEmployee(AddEmployeeDto addEmployeeDto);

        Task<List<GetEmployeeDto>> GetAllEmployeeData();

        Task<GetEmployeeDto> GetEmployeeDataByName(string EmployeeName);

        Task<bool> DeleteEmployee(int EmployeeId);

        Task<bool> UpdateEmployee(int EmployeeId, AddEmployeeDto addEmployeeDto);

    }
}
