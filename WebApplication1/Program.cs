using Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Extensions;
using NLog;
using WebApi.Extensions;
using Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentations.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config")); //nlog configuration

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;   //Content Negotiation
})
    .AddXmlDataContractSerializerFormatters() // for xml request
    .AddApplicationPart(typeof(Presentations.AssemlyReference).Assembly);
   //.AddNewtonsoftJson();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddMvc();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggingService>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
