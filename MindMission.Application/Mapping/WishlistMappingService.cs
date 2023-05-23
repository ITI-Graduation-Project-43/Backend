using MindMission.Application.DTOs;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using Stripe;


namespace MindMission.Application.Mapping
{
    public class WishlistMappingService : IMappingService <Wishlist , WishlistDto>
    {
        private readonly ICourseService _CourseService;
        public WishlistMappingService(ICourseService courseService)
        {
            _CourseService= courseService;
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
                wishlistDto.CoursePrice = course.Price;
            }
            if (wishlist.Student != null)
            {
                wishlistDto.StudentId = wishlist.StudentId;
                wishlistDto.StudentName = wishlist.Student.FirstName + " " + wishlist.Student.LastName;
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
