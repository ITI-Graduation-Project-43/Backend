using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class WishlistMappingService : IMappingService<Wishlist, WishlistDto>
    {



        public async Task<WishlistDto> MapEntityToDto(Wishlist wishlist)
        {
            var wishlistDto = new WishlistDto
            {
                Id = wishlist.Id,
                AddedDate = wishlist.AddedDate,
                CourseId = wishlist.Course.Id,
                CourseTitle = wishlist.Course.Title,
                CourseDescription = wishlist.Course.Description,
                CourseImageUrl = wishlist.Course.ImageUrl,
                CoursePrice = wishlist.Course.Price,
                CourseAvgReview = wishlist.Course.AvgReview,
                CategoryName = wishlist.Course.Category.Name,
                StudentId = wishlist.Student.Id,
                StudentName = wishlist.Student.FullName
            };

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