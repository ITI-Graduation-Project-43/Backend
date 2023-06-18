using AutoMapper;
using MindMission.Application.DTOs.VideoDtos;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapper
{
    /// <summary>
    /// This class is used by AutoMapper to understand how to map from the Video entity to the corresponding DTOs and vice versa.
    /// </summary>
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoDto>()
                .ForMember(dest => dest.LessonName, opts => opts.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.LessonDescription, opt => opt.MapFrom(src => src.Lesson.Description));

            CreateMap<VideoCreateDto, Video>();
            CreateMap<Video, VideoCreateDto>();

        }
    }
}
