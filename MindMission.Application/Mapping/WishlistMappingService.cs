using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class WishlistMappingService : IMappingService <Wishlist , WishlistDto>
    {
        private readonly ICourseService _CourseService;
        private readonly IStudentService _StudentService;
        public WishlistMappingService(ICourseService courseService, IStudentService studentService)
        {
            _CourseService = courseService;
            _StudentService = studentService;
        }
        public async Task<WishlistDto> MapEntityToDto(Wishlist wishlist)
        {
            var wishlistDto = new WishlistDto
            {
                Id = wishlist.Id,
                AddedDate = wishlist.AddedDate
            };
            var course = await _CourseService.GetByIdAsync(wishlist.CourseId);
            if ( course != null)
            {
                wishlistDto.CourseId = course.Id;
                wishlistDto.CourseTitle = course.Title;
                wishlistDto.CourseImageUrl = course.ImageUrl;
                wishlistDto.CoursePrice = course.Price;
            }
            var student = await _StudentService.GetByIdAsync(wishlist.StudentId);
            if (student != null)
            {
                wishlistDto.StudentId = student.Id;
                wishlistDto.StudentName = student.FirstName + " " + student.LastName;
            }
            return wishlistDto;
        }

        public Wishlist MapDtoToEntity(WishlistDto wishlistDto)
        {
            return new Wishlist
            {
                Id = wishlistDto.Id,
                AddedDate = wishlistDto.AddedDate,
                CourseId = wishlistDto.CourseId,
                StudentId = wishlistDto.StudentId
            };
        }

    }
}
