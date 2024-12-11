using EmployeeApi.Middelware;
using EmployeeDomain.DependenacyInjection;
using EmployeeInfrastructure.DbConfigure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Access the connection string from the configuration system
string connectionString = builder.Configuration.GetConnectionString("EmployeeDb");

//Add dependenacy injection
AddDependeancyInjection.AddDependeancy(builder.Services);

//Add Dbcontext layer configuration
ConfigureDbContext.AddDbContext(builder.Services, connectionString);

//Add validation
InjectValidator.AddValidator(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

GlobalExceptionMiddlewareExtensions.UseGlobalExceptionMiddleware(app);

app.MapControllers();

app.Run();
