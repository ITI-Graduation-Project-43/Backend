using Microsoft.AspNetCore.Http;
using MindMission.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class AttachmentDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public FileType FileType { get; set; }

        [Required]
        public int LessonId { get; set; }

    }
}