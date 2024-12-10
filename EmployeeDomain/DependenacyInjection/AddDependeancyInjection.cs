using EmployeeDomain.Interfaces;
using EmployeeDomain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDomain.DependenacyInjection
{
    public static class AddDependeancyInjection
    {
        public static IServiceCollection AddDependeancy(IServiceCollection services)
        {
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            return services;
        }

    }
}
