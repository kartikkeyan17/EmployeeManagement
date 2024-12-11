using EmployeeDomain.Utilties.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeDomain.DependenacyInjection
{
    public static class InjectValidator
    {
        public static IServiceCollection AddValidator(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();

            return services;
        }
    }
}
