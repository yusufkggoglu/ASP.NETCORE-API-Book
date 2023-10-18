using Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddApplicationPart(typeof(Presentations.AssemlyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
