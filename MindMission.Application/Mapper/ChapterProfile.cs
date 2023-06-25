using AutoMapper;
using MindMission.Application.DTOs.CourseChapters;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapper
{
    public class ChapterProfile : Profile
    {
        public ChapterProfile()
        {
            CreateMap<Chapter, CreateChapterDto>().ReverseMap();
        }
    }
}
