using MindMission.Application.CustomValidation.DataAnnotation;
using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs.PermissionDtos
{
    public class PermissionCreateDto
    {
        public int Id { get; set; }
        [Required]
        [RangeValueAttribute(2, 100)]
        [Alphabetic]
        public string Name { get; set; } = string.Empty;

        [RangeValueAttribute(2, 1000)]

        public string Description { get; set; } = string.Empty;
    }
}
