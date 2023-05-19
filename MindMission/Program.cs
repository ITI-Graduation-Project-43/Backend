using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Services;
using MindMission.Infrastructure;
using MindMission.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MindMissionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MindMissionDb"),
        b => b.MigrationsAssembly("MindMission.API"));
});
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
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

app.MapControllers();

app.Run();
