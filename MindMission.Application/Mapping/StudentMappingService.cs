using MindMission.Application.DTOs;
using MindMission.Application.DTOs.Account;
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
                NoOfCourses = studentDto.NoOfCourses,
                NoOfWishlist = studentDto.NoOfWishlist,
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
                NoOfCourses = entity.NoOfCourses,
                NoOfWishlist = entity.NoOfWishlist
            };

            var userAccounts = await _userAccountService.GetUserAccountsAsync(entity.Id) ?? throw new ArgumentNullException(nameof(_userAccountService));
            foreach (var account in userAccounts)
            {
                studentDto.Accounts.Add(new AccountDto() { Id = account.Id, AccountId = account.AccountId, AccountName = account.Account.AccountName, AccountDomain = account.AccountLink });
            }

            return studentDto;
        }
    }
}