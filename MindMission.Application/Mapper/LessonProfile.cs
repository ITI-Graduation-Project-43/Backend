using AutoMapper;
using MindMission.Application.DTOs.CourseChapters;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapper
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();
        }
    }
}
