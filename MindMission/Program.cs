using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MindMission.API.EmailSettings;
using MindMission.API.Middlewares;
using MindMission.API.Utilities.Identity.IdentityPolicy;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Patch;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Mapping.Post;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories;
using Stripe;
using System.Text;
using MindMission.Application.CustomValidation;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Validator.Base;
using MindMission.Application.Validator;
using MindMission.Application.DTOs.QuizDtos;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.DTOs.VideoDtos;
using MindMission.Application.Service.Interfaces;
using MindMission.Application.DTOs.QuestionDtos;
using MindMission.Application.DTOs.UserDtos;
using AccountService = MindMission.Application.Services.AccountService;
using MindMission.Application.Services.Upload;
using MindMission.Application.Interfaces.Azure_services;
using MindMission.Application.Services.Azure_services;
using MindMission.Application.DTOs.CourseChapters;
using MindMission.Application.DTOs.AttachmentDtos;
using MindMission.Infrastructure.SignalR;

string TextCore = "Messi";
var builder = WebApplication.CreateBuilder(args);

#region serilog
//var Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(Logger);
#endregion


builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSignalR();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddDbContext<MindMissionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MindMissionDbOnline"),
        b => b.MigrationsAssembly("MindMission.API"));

    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution); // To prevent create 2 copy for the same object at one-to-many relationship case which make the memory more better but decrease in the performance due to it check if the object is created before or not
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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
/*  upload files */
builder.Services.AddScoped<IUploadImageService, UploadImageService>();
builder.Services.AddScoped<IUploadVideoService, UploadVideoService>();
builder.Services.AddScoped<IUploadAttachmentService, UploadAttachmentService>();
builder.Services.AddScoped<IDeleteService, DeleteUploadedServices>();
builder.Services.AddScoped<IDownloadService, DownloadService>();



/*Admin Configuration*/
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<AdminMappingService, AdminMappingService>();

#region Chapter 
/*Chapter Configuration*/
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<IChapterLessonsService, ChapterLessonsService>();
builder.Services.AddScoped<IMappingService<Chapter, ChapterDto>, ChapterMappingService>();
builder.Services.AddScoped<IMappingService<Chapter, PostChapterDto>, PostChapterMappingService>();
builder.Services.AddScoped<IValidatorService<CreateChapterDto>, ChapterValidatorService>();

#endregion

#region Lesson 
/*Lesson Configuration*/
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IMappingService<Lesson, LessonDto>, LessonMappingService>();
builder.Services.AddScoped<IMappingService<Question, PostQuestionDto>, PostQuestionMappingService>();
builder.Services.AddScoped<IMappingService<Lesson, PostQuizLessonDto>, PostQuizLessonMappingService>();
builder.Services.AddScoped<IMappingService<Lesson, PostArticleLessonDto>, PostArticleLessonMappingService>();
builder.Services.AddScoped<IMappingService<Lesson, PostVideoLessonDto>, PostVideoLessonMappingService>();
builder.Services.AddScoped<IArticleLessonPatchValidator, ArticleLessonPatchValidator>();
builder.Services.AddScoped<IQuizLessonPatchValidator, QuizLessonPatchValidator>();
builder.Services.AddScoped<IVideoLessonPatchValidator, VideoLessonPatchValidator>();
builder.Services.AddScoped<IValidatorService<CreateLessonDto>, LessonValidatorService>();

#endregion

#region Attachment 
/*Attachment Configuration*/
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IAttachmentMappingService, AttachmentMappingService>();
builder.Services.AddScoped<IValidatorService<AttachmentCreateDto>, AttachmentValidatorService>();
#endregion

#region Article 
/*Article Configuration*/
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IValidatorService<ArticleCreateDto>, ArticleValidatorService>();
#endregion

#region Video 
/*Video Configuration*/
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IValidatorService<VideoCreateDto>, VideoValidatorService>();
#endregion

#region Quiz 
/*Quiz Configuration*/
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IValidatorService<QuizCreateDto>, QuizValidatorService>();

#endregion

#region Question 
/*Question Configuration*/
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IValidatorService<QuestionCreateDto>, QuestionValidatorService>();
#endregion


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
builder.Services.AddScoped<ICoursePatchValidator, CoursePatchValidator>();
builder.Services.AddScoped<IMappingService<Course, CourseDto>, CourseMappingService>();
builder.Services.AddScoped<IMappingService<Course, PostCourseDto>, PostCourseMappingService>();
builder.Services.AddScoped<IMappingService<CourseRequirement, CourseRequirementCreateDto>, CourseRequirementMappingService>();
builder.Services.AddScoped<IMappingService<EnrollmentItem, EnrollmentItemCreateDto>, EnrollmentItemMappingService>();
builder.Services.AddScoped<IMappingService<LearningItem, LearningItemCreateDto>, LearningItemMappingService>();

/*Instructor Configuration*/
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<InstructorMappingService, InstructorMappingService>();
builder.Services.AddScoped<IMappingService<Instructor, InstructorDto>, InstructorMappingService>();

/*Account Configuration*/
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<AccountMappingService, AccountMappingService>();

/*UserAccount Configuration*/
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<UserAccountMappingService, UserAccountMappingService>();
builder.Services.AddScoped<IMappingService<UserAccount, UserAccountDto>, UserAccountMappingService>();

/*Wishlist Configuration*/
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IMappingService<Wishlist, WishlistDto>, WishlistMappingService>();

/*TimeTracking Configuration*/
builder.Services.AddScoped<ITrackingTimeRepository, TimeTrackingRepository>();
builder.Services.AddScoped<ITimeTrackingService, TimeTrackingService>();

/*Enrollment Configuration*/
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<EnrollmentMappingService, EnrollmentMappingService>();
builder.Services.AddScoped<IMappingService<Enrollment, EnrollmentDto>, EnrollmentMappingService>();



/*Student Configuration*/
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IMappingService<Student, StudentDto>, StudentMappingService>();




/*Coupon Configuration*/
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<ICouponService, MindMission.Application.Services.CouponService>();
builder.Services.AddScoped<IMappingService<MindMission.Domain.Models.Coupon, CouponDto>, SiteCouponMappingService>();

/*Site Coupon Configuration*/
builder.Services.AddScoped<ISiteCouponRepository, SiteCouponRepository>();
builder.Services.AddScoped<ISiteCouponService, SiteCouponService>();


/*User Configuration*/
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMappingService<User, UserDto>, UserMappingService>();
/*Message Configuration*/
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<MessageMappingService, MessageMappingService>();
builder.Services.AddScoped<IMappingService<Messages, MessageDto>, MessageMappingService>();

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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();


app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200", "http://localhost:60998"));


app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.MapHub<DiscussionHub>("/discussionHub");

app.Run();