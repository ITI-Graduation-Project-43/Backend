using AutoMapper;
using MindMission.Application.DTOs.AttachmentDtos;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapper
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment, AttachmentCreateDto>()
                       .ForMember(dest => dest.AttachmentUrl, opt => opt.MapFrom(src => src.Url))
                       .ForMember(dest => dest.AttachmentName, opt => opt.MapFrom(src => src.Name))
                       .ForMember(dest => dest.AttachmentType, opt => opt.MapFrom(src => src.Type))
                       .ForMember(dest => dest.AttachmentSize, opt => opt.MapFrom(src => src.Size))
                       .ReverseMap()
                       .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.AttachmentUrl))
                       .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AttachmentName))
                       .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.AttachmentType))
                       .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.AttachmentSize));
        }
    }
}
