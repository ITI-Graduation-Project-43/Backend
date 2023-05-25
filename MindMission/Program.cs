using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories;
using Stripe;


string TextCore = "Messi";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MindMissionDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("MindMissionDbOnline"),
        b => b.MigrationsAssembly("MindMission.API"));
    options.EnableSensitiveDataLogging();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


});





builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();

builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();
builder.Services.AddScoped<IDiscussionService, DiscussionService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<CategoryMappingService, CategoryMappingService>();
builder.Services.AddScoped<IMappingService<Category, CategoryDto>, CategoryMappingService>();


builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<CourseMappingService, CourseMappingService>();
builder.Services.AddScoped<IMappingService<Course, CourseDto>, CourseMappingService>();


builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<InstructorMappingService, InstructorMappingService>();
builder.Services.AddScoped<IMappingService<Instructor, InstructorDto>, InstructorMappingService>();

builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<UserAccountMappingService, UserAccountMappingService>();
builder.Services.AddScoped<IMappingService<UserAccount, UserAccountDto>, UserAccountMappingService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<WishlistMappingService, WishlistMappingService>();
builder.Services.AddScoped<IMappingService<Wishlist, WishlistDto>, WishlistMappingService>();

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<EnrollmentMappingService, EnrollmentMappingService>();
builder.Services.AddScoped<IMappingService<Enrollment, EnrollmentDto>, EnrollmentMappingService>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<AdminMappingService, AdminMappingService>();
builder.Services.AddScoped<IMappingService<Admin, AdminDto>, AdminMappingService>();

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MindMissionDbContext>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<StudentMappingService, StudentMappingService>();
builder.Services.AddScoped<IMappingService<Student, StudentDto>, StudentMappingService>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<QuestionMappingService, QuestionMappingService>();
builder.Services.AddScoped<IMappingService<Question, QuestionDto>, QuestionMappingService>();

builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<QuizMappingService, QuizMappingService>();
builder.Services.AddScoped<IMappingService<Quiz, QuizDto>, QuizMappingService>();

builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IChapterService, ChapterService>();
builder.Services.AddScoped<ChapterMappingService, ChapterMappingService>();
builder.Services.AddScoped<IMappingService<Chapter, ChapterDto>, ChapterMappingService>();

builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<LessonMappingService, LessonMappingService>();
builder.Services.AddScoped<IMappingService<Lesson, LessonDto>, LessonMappingService>();

builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IAttachmentService, AttachmentService>();

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




// Stripe Service Registeration
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<ChargeService, ChargeService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<CustomerService, CustomerService>();
StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeSettings:SecretKey");


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
