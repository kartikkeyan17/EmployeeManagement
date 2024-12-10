using System;
using System.Collections.Generic;

namespace EmployeeInfrastructure.Models;

public partial class DesignationMaster
{
    public int DesignationId { get; set; }

    public string? Designation { get; set; }

    public bool Active { get; set; }

    public bool DeleteFlag { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
