using MindMission.Application.DTOs;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public class InstructorMappingService : IMappingService<Instructor, InstructorDto>
    {
        public Instructor MapDtoToEntity(InstructorDto instructorDto)
        {
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
                UpdatedAt = null
            };
        }

        public async Task<InstructorDto> MapEntityToDto(Instructor entity)
        {
            var InstructorDTO = new InstructorDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Bio = entity.Bio,
                Title = entity.Title,
                Description = entity.Description,
                NoOfCources = entity.NoOfCourses,
                AvgRating = (double)entity.AvgRating,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                NoOfStudents = entity.NoOfStudents,
                ProfilePicture = entity.ProfilePicture,
                NoOfRating = entity.NoOfRatings
            };
            return InstructorDTO;
        }
    }
}
