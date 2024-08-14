using BookingSystem.Api.Common.Errors;
using BookingSystem.Application;
using BookingSystem.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
        
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, BookingSystemProblemDetailsFactory>();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}