using AutoMapper;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.DTOs.AttachmentDtos;
using MindMission.Application.DTOs.CourseChapters;
using MindMission.Application.DTOs.QuestionDtos;
using MindMission.Application.DTOs.QuizDtos;
using MindMission.Application.DTOs.VideoDtos;
using MindMission.Application.Exceptions;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service.Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Validator.Base;
using MindMission.Domain.Models;
using System.Transactions;

namespace MindMission.Application.Services
{
    public class ChapterLessonsService : IChapterLessonsService
    {
        private readonly IMapper _mapper;
        private readonly IChapterRepository _chapterRepository;


        public ChapterLessonsService(
             IMapper mapper,
             IChapterRepository chapterRepository)
        {
            _mapper = mapper;
            _chapterRepository = chapterRepository;
        }
        public async Task AddChapters(List<CreateChapterDto> chapterDtos)
        {

            using var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(5)
            }, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                foreach (var chapterDto in chapterDtos)
                {
                    var chapter = _mapper.Map<Chapter>(chapterDto);
                    chapter.NoOfLessons = chapterDto.Lessons.Count;
                    chapter.NoOfHours = chapterDto.Lessons.Sum(lesson => lesson.NoOfHours);
                    await _chapterRepository.AddAsync(chapter);
                }

                transaction.Complete();
            }
            catch
            {
                transaction.Dispose();
                throw;
            }
        }

    }
}
