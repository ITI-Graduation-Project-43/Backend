using Microsoft.AspNetCore.Http;
using MindMission.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
