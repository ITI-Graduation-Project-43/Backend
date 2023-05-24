﻿using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Services
{
    public interface IAttachmentService
    {
        public Task<Attachment> AddAttachmentAsync(AttachmentDto attachmentDto);
        public Task<bool> IsExistedLesson(int id);
    }
}
