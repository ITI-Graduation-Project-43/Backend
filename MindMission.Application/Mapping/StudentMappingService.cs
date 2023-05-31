using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class StudentMappingService : IMappingService<Student, StudentDto>
    {
        private readonly IUserAccountService _userAccountService;

        public StudentMappingService(IUserAccountService context)
        {
            _userAccountService = context;
        }

        public Student MapDtoToEntity(StudentDto dto)
        {
            return new Student
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Bio = dto.Bio,
                ProfilePicture = dto.ProfilePicture,
                NumCourses = dto.NumCourses,
                NumWishlist = dto.NumWishlist,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };
        }

        public async Task<StudentDto> MapEntityToDto(Student entity)
        {
            var StudentDTO = new StudentDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Bio = entity.Bio,
                ProfilePicture = entity.ProfilePicture,
                NumCourses = entity.NumCourses,
                NumWishlist = entity.NumWishlist,
            };
            var UserAccounts = _userAccountService.GetUserAccountsAsync(entity.Id);
            foreach (var account in UserAccounts)
            {
                StudentDTO.accounts.Add(account.Account.AccountType, account.AccountLink);
            }
            return StudentDTO;
        }
    }
}