using Microsoft.AspNetCore.Http;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IAttachmentRepository
    {
        Task PostAttachmentAsync(Lesson lesson, Attachment attachment);
        public Task<Attachment?> GetAttachmentByIdAsync(int id);
    }
}
