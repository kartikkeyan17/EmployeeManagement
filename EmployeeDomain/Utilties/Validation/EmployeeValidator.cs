using System;
using EmployeeDomain.Dto;
using EmployeeInfrastructure.Models;
using FluentValidation;

namespace EmployeeDomain.Utilties.Validation
{
    public class EmployeeValidator : AbstractValidator<AddEmployeeDto>
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeValidator(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            RuleFor(r => r.DepartmentId).NotNull().NotEmpty().WithMessage("Department Id is missing")
                .Must((r) => CheckDepartmentId(r)).WithMessage("Department id is not exist database").When(w => w.DepartmentId != 0);

            RuleFor(r => r.DesignationnId).NotNull().NotEmpty().WithMessage("Designation Id is missing")
              .Must((r) => CheckDesignationId(r)).WithMessage("Designation id is not exist database").When(w => w.DesignationnId != 0);
        }

        public bool CheckDepartmentId(int departmentId)
        {
            bool isExist = _employeeContext.DepartmentMasters.Any(a => a.DepartmentId == departmentId && a.Active == true && a.DeleteFlag == false);
            return isExist;
        }
        public bool CheckDesignationId(int designationId)
        {
            bool isExist = _employeeContext.DesignationMasters.Any(a => a.DesignationId == designationId && a.Active == true && a.DeleteFlag == false);
            return isExist;
        }
    }
}
