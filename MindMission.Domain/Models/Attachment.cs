﻿using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Enums;
using MindMission.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MindMission.Domain.Models
{
    /// <summary>
    /// Represents an attachment entity that belongs to a lesson.
    /// </summary>
    [Index(nameof(LessonId), Name = "idx_attachments_lessonid")]
    public partial class Attachment : BaseEntity, IEntity<int>, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LessonId { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;


        [Required]
        public byte[] FileData { get; set; } = new byte[1];
        public bool IsDeleted { get; set; } = false;


        [Required]
        [EnumDataType(typeof(FileType))]
        public string FileType { get; set; } = string.Empty;

        [ForeignKey(nameof(LessonId))]
        [InverseProperty("Attachment")]
        public virtual Lesson Lesson { get; set; } = null!;
    }
}