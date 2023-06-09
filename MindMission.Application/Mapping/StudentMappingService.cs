using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class StudentMappingService : IMappingService<Student, StudentDto>
    {
        private readonly IUserAccountService _userAccountService;

        public StudentMappingService(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService ?? throw new ArgumentNullException(nameof(userAccountService));
        }

        public Student MapDtoToEntity(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                throw new ArgumentNullException(nameof(studentDto));
            }
            return new Student
            {
                Id = studentDto.Id,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Bio = studentDto.Bio,
                ProfilePicture = studentDto.ProfilePicture,
                NumCourses = studentDto.NumCourses,
                NumWishlist = studentDto.NumWishlist,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public async Task<StudentDto> MapEntityToDto(Student entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var studentDto = new StudentDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Bio = entity.Bio,
                ProfilePicture = entity.ProfilePicture,
                NumCourses = entity.NumCourses,
                NumWishlist = entity.NumWishlist
            };

            var userAccounts = await _userAccountService.GetUserAccountsAsync(entity.Id) ?? throw new ArgumentNullException(nameof(_userAccountService));
            foreach (var account in userAccounts)
            {
                studentDto.accounts.Add(account.Account.AccountType, account.AccountLink);
            }

            return studentDto;
        }
    }
}