using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MindMission.API.EmailSettings;
using MindMission.API.Middlewares;
using MindMission.API.Utilities;
using MindMission.API.Utilities.Identity.IdentityPolicy;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Mapping.Post;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories;
using Serilog;
using Stripe;
using System.Text;

string TextCore = "Messi";
var builder = WebApplication.CreateBuilder(args);

#region serilog
var Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Logger);
#endregion


builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddRazorPages();

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddDbContext<MindMissionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MindMissionDbOnline"),
        b => b.MigrationsAssembly("MindMission.API"));

    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

/*JWT Configuration*/
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

/*Azure Configuration*/
builder.Services.AddScoped(x =>
{
    var configuration = x.GetRequiredService<IConfiguration>();
    string connectionString = configuration["AzureStorage:ConnectionString"];
    return new BlobServiceClient(connectionString);
});

/*Admin Configuration*/
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<AdminMappingService, AdminMappingService>();

/*Permission Configuration*/
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<PermissionMappingService, PermissionMappingService>();
builder.Services.AddScoped<IMappingService<Permission, PermissionDto>, PermissionMappingService>();

/*Discussion Configuration*/
builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();
builder.Services.AddScoped<IDiscussionService, DiscussionService>();
builder.Services.AddScoped<DiscussionMappingService, DiscussionMappingService>();
builder.Services.AddScoped<IMappingService<Discussion, DiscussionDto>, DiscussionMappingService>();

/*Category Configuration*/
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<CategoryMappingService, CategoryMappingService>();
builder.Services.AddScoped<IMappingService<Category, CategoryDto>, CategoryMappingService>();

/*Course Configuration*/
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<CourseMappingService, CourseMappingService>();
builder.Services.AddScoped<IMappingService<Course, CourseDto>, CourseMappingService>();
builder.Services.AddScoped<IMappingService<Course, CourseCreateDto>, PostCourseMappingService>();
builder.Services.AddScoped<IMappingService<CourseRequirement, CourseRequirementCreateDto>, CourseRequirementMappingService>();
builder.Services.AddScoped<IMappingService<EnrollmentItem, EnrollmentItemCreateDto>, EnrollmentItemMappingService>();
builder.Services.AddScoped<IMappingService<LearningItem, LearningItemCreateDto>, LearningItemMappingService>();


/*Instructor Configuration*/
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<InstructorMappingService, InstructorMappingService>();
builder.Services.AddScoped<IMappingService<Instructor, InstructorDto>, InstructorMappingService>();

/*UserAccount Configuration*/
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<UserAccountMappingService, UserAccountMappingService>();
builder.Services.AddScoped<IMappingService<UserAccount, UserAccountDto>, UserAccountMappingService>();

/*Wishlist Configuration*/
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<WishlistMappingService, WishlistMappingService>();
builder.Services.AddScoped<IMappingService<Wishlist, WishlistDto>, WishlistMappingService>();

/*Enrollment Configuration*/
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<EnrollmentMappingService, EnrollmentMappingService>();
builder.Services.AddScoped<IMappingService<Enrollment, EnrollmentDto>, EnrollmentMappingService>();

/*Article Configuration*/
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ArticleMappingService, ArticleMappingService>();
builder.Services.AddScoped<IMappingService<Article, ArticleDto>, ArticleMappingService>();

/*Student Configuration*/
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<StudentMappingService, StudentMappingService>();
builder.Services.AddScoped<IMappingService<Student, StudentDto>, StudentMappingService>();

/*Question Configuration*/
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<QuestionMappingService, QuestionMappingService>();
builder.Services.AddScoped<IMappingService<Question, QuestionDto>, QuestionMappingService>();

/*Quiz Configuration*/
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<QuizMappingService, QuizMappingService>();
builder.Services.AddScoped<IMappingService<Quiz, QuizDto>, QuizMappingService>();

/*Chapter Configuration*/
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<ChapterMappingService, ChapterMappingService>();
builder.Services.AddScoped<IMappingService<Chapter, ChapterDto>, ChapterMappingService>();

/*Lesson Configuration*/
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<LessonMappingService, LessonMappingService>();
builder.Services.AddScoped<IMappingService<Lesson, LessonDto>, LessonMappingService>();

/*Attachment Configuration*/
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IAttachmentMappingService, AttachmentMappingService>();

/*User Configuration*/
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMappingService<User, UserDto>, UserMappingService>();

/*Identity Configuration*/
builder.Services.AddTransient<IUserValidator<User>, CustomUserValidator>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MindMissionDbContext>().AddDefaultTokenProviders().AddTokenProvider<DataProtectorTokenProvider<User>>("Default");
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
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

/*Stripe Service Configuration*/
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<ChargeService, ChargeService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<CustomerService, CustomerService>();
StripeConfiguration.ApiKey = builder.Configuration["StripeSettings:SecretKey"];

/*CourseFeedback Configuration*/
builder.Services.AddScoped<ICourseFeedbackRepository, CourseFeedbackRepository>();
builder.Services.AddScoped<ICourseFeedbackService, CourseFeedbackService>();
builder.Services.AddScoped<CourseFeedbackMappingService, CourseFeedbackMappingService>();

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

// Stripe Service Registration
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<IPaymentMappingService, PaymentMappingService>();
builder.Services.AddScoped<ChargeService, ChargeService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<CustomerService, CustomerService>();
StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeSettings:SecretKey");

builder.Services.AddTransient<ExceptionMiddleware>();

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

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();