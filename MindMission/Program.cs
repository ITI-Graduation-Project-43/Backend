using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MindMission.API.EmailSettings;
using MindMission.API.Utilities.Identity.IdentityPolicy;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories;
using Stripe;
using System.Text;

string TextCore = "Messi";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // To authenticate using token instead of cookie
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true; //...............
    options.RequireHttpsMetadata = false; // To check the token come from https or not (true => will check)
    options.TokenValidationParameters = new TokenValidationParameters() //Give it the parameters that will be used in validation such as signing key, Issuers, Audiences,.......
    {
        ValidateIssuer = false,
        ValidIssuer = builder.Configuration["JWT:ValidIssuers"],
        ValidateAudience = false,
        ValidAudience = builder.Configuration["JWT:ValidAudiences"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});

builder.Services.AddControllers();

builder.Services.AddRazorPages();

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddDbContext<MindMissionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MindMissionDbOnline"),
        b => b.MigrationsAssembly("MindMission.API"));
});

//builder.Services.AddAuthorization();

/*Permission Configuration*/
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

/*Discussion Configuration*/
builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();
builder.Services.AddScoped<IDiscussionService, DiscussionService>();

/*Category Configuration*/
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

/*Course Configuration*/
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<CourseMappingService, CourseMappingService>();
builder.Services.AddScoped<IMappingService<Course, CourseDto>, CourseMappingService>();

/*Instructor Configuration*/
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();

/*User Configuration*/
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMappingService<User, UserDto>, UserMappingService>();

/*Identity Configuration*/
builder.Services.AddTransient<IUserValidator<User>, CustomUserValidator>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MindMissionDbContext>().AddDefaultTokenProviders().AddTokenProvider<DataProtectorTokenProvider<User>>("Default");
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    //options.Password.RequireDigit = true;
    //options.Password.RequireLowercase = true;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequireUppercase = true;
    //options.Password.RequiredUniqueChars = 1;

});

/*Mail Configuration*/
builder.Services.AddTransient<IMailService, MailService>();

/*Stripe Service Registration*/
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<ChargeService, ChargeService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<CustomerService, CustomerService>();
StripeConfiguration.ApiKey = builder.Configuration["StripeSettings:SecretKey"];

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(Swagger =>
{
    Swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "APS.Net 6 Web API",
        Description = "MindMission Policy",
        Contact = new OpenApiContact
        {
            Name = "MindMission Contact",
            Email = "mind.mission.site@gmail.com",
            Url = new Uri("https://example.com/contact"),
        }
    });

    Swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter Bearer [space] and then valid token"
    });

    Swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseCors(TextCore);

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
