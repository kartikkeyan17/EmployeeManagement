namespace EmployeeDomain.Dto
{
    public class AddEmployeeDto
    {
        public string EmployeeName { get; set; }

        public int DepartmentId { get; set; }

        public int DesignationnId { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }
    }
}
