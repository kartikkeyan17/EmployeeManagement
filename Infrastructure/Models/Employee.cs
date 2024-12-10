using System;
using System.Collections.Generic;

namespace EmployeeInfrastructure.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int DepartmentId { get; set; }

    public int DesignationId { get; set; }

    public string? EmployeeName { get; set; }

    public string? Email { get; set; }

    public decimal? Salary { get; set; }

    public bool Active { get; set; }

    public bool DeleteFlag { get; set; }

    public virtual DepartmentMaster Department { get; set; } = null!;

    public virtual DesignationMaster Designation { get; set; } = null!;
}
