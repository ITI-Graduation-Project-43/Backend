using AutoMapper;
using MindMission.Application.DTOs.AttachmentDtos;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapper
{
    public class AttachmentProfile : Profile
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment, AttachmentCreateDto>().ReverseMap();
        }
    }
}
