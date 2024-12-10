using EmployeeInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInfrastructure.DbConfigure
{
    public static class ConfigureDbContext
    {

        public static IServiceCollection AddDbContext(IServiceCollection services, string ConnectionString)
        {

            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
            return services;
        }

    }
}
