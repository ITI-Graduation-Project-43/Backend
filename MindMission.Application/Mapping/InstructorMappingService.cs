using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class InstructorMappingService : IMappingService<Instructor, InstructorDto>
    {
        private readonly IUserAccountService _userAccountService;

        public InstructorMappingService(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService ?? throw new ArgumentNullException(nameof(userAccountService));
        }

        public Instructor MapDtoToEntity(InstructorDto instructorDto)
        {
            if (instructorDto == null)
            {
                throw new ArgumentNullException(nameof(instructorDto));
            }

            return new Instructor
            {
                Id = instructorDto.Id,
                FirstName = instructorDto.FirstName,
                LastName = instructorDto.LastName,
                Bio = instructorDto.Bio,
                Title = instructorDto.Title,
                Description = instructorDto.Description,
                NoOfCourses = instructorDto.NoOfCourses,
                NoOfRatings = instructorDto.NoOfStudents,
                NoOfStudents = instructorDto.NoOfStudents,
                AvgRating = instructorDto.AvgRating,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
        }

        public async Task<InstructorDto> MapEntityToDto(Instructor entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var instructorDto = new InstructorDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Bio = entity.Bio,
                Title = entity.Title,
                Description = entity.Description,
                NoOfCources = entity.NoOfCourses,
                AvgRating = entity.AvgRating.HasValue ? entity.AvgRating.Value : 0,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                NoOfStudents = entity.NoOfStudents,
                ProfilePicture = entity.ProfilePicture,
                NoOfRating = entity.NoOfRatings
            };

            if (entity.Courses != null)
            {
                foreach (var course in entity.Courses)
                {
                    var courseDict = new Dictionary<string, string>
                {
                    { "title", course.Title },
                    { "description", course.ShortDescription },
                    { "NoOfStudents", $"{course.NoOfStudents}" },
                    { "Price", $"{course.Price}" }
                };
                    instructorDto.Courses.Add(courseDict);
                }
            }

            var userAccounts = await _userAccountService.GetUserAccountsAsync(entity.Id) ?? throw new ArgumentNullException(nameof(_userAccountService));
            foreach (var account in userAccounts)
            {
                instructorDto.accounts.Add(account.Account.AccountName, account.AccountLink);
            }

            return instructorDto;
        }
    }

}