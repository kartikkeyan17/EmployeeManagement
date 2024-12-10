using System;
using System.Collections.Generic;

namespace EmployeeInfrastructure.Models;

public partial class DepartmentMaster
{
    public int DepartmentId { get; set; }

    public string? Department { get; set; }

    public bool Active { get; set; }

    public bool DeleteFlag { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
