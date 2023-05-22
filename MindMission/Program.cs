using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;
using MindMission.Infrastructure;
using MindMission.Infrastructure.Repositories;

string TextCore = "Messi";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MindMissionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MindMissionDbOnline"),
        b => b.MigrationsAssembly("MindMission.API"));
});
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();
builder.Services.AddScoped<IDiscussionService, DiscussionService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MindMissionDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
    option.AddPolicy(TextCore,
        builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowAnyOrigin();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(TextCore);

app.MapControllers();

app.Run();
