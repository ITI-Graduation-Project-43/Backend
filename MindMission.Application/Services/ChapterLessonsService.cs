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

        private readonly IValidatorService<CreateChapterDto> _chapterValidatorService;
        private readonly IValidatorService<CreateLessonDto> _lessonValidatorService;
        private readonly IValidatorService<AttachmentCreateDto> _attachmentValidatorService;
        private readonly IValidatorService<ArticleCreateDto> _articleValidatorService;
        private readonly IValidatorService<VideoCreateDto> _videoValidatorService;
        private readonly IValidatorService<QuizCreateDto> _quizValidatorService;
        private readonly IValidatorService<QuestionCreateDto> _questionValidatorService;



        private readonly IChapterRepository _chapterRepository;
        private readonly ILessonService _lessonService;
        private readonly IAttachmentService _attachmentService;
        private readonly IVideoService _videoService;
        private readonly IArticleService _articleService;
        private readonly IQuizService _quizService;
        private readonly IQuestionService _questionService;

        public ChapterLessonsService(
             IMapper mapper,
             IValidatorService<CreateChapterDto> chapterValidatorService,
             IValidatorService<CreateLessonDto> lessonValidatorService,
             IValidatorService<AttachmentCreateDto> attachmentValidatorService,
             IValidatorService<ArticleCreateDto> articleValidatorService,
             IValidatorService<VideoCreateDto> videoValidatorService,
             IValidatorService<QuizCreateDto> quizValidatorService,
             IValidatorService<QuestionCreateDto> questionValidatorService,
             IChapterRepository chapterRepository,
             ILessonService lessonService,
             IAttachmentService attachmentService,
             IVideoService videoService,
             IArticleService articleService,
             IQuizService quizService,
             IQuestionService questionService)
        {
            _mapper = mapper;
            _chapterRepository = chapterRepository;
            _lessonService = lessonService;
            _attachmentService = attachmentService;
            _videoService = videoService;
            _articleService = articleService;
            _quizService = quizService;
            _questionService = questionService;
            _chapterValidatorService = chapterValidatorService;
            _lessonValidatorService = lessonValidatorService;
            _attachmentValidatorService = attachmentValidatorService;
            _articleValidatorService = articleValidatorService;
            _videoValidatorService = videoValidatorService;
            _quizValidatorService = quizValidatorService;
            _questionValidatorService = questionValidatorService;

        }

        public async Task AddChapters(List<CreateChapterDto> chapterDtos)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                foreach (var chapterDto in chapterDtos)
                {
                    var chapterValidationResult = await ValidateChapterAsync(chapterDto);
                    if (chapterValidationResult != null)
                    {
                        throw new ApiException(chapterValidationResult);
                    }

                    var chapter = _mapper.Map<Chapter>(chapterDto);
                    chapter.NoOfLessons = chapterDto.Lessons.Count;

                    var createdChapter = await _chapterRepository.AddAsync(chapter);

                    foreach (var lessonDto in chapterDto.Lessons)
                    {
                        lessonDto.ChapterId = createdChapter.Id;
                        var lessonValidationResult = await ValidateLessonAsync(lessonDto);
                        if (lessonValidationResult != null)
                        {
                            throw new ApiException(lessonValidationResult);
                        }

                        var lesson = _mapper.Map<Lesson>(lessonDto);
                        var createdLesson = await _lessonService.AddAsync(lesson);

                        if (lessonDto.Attachment != null)
                        {
                            lessonDto.Attachment.LessonId = createdLesson.Id;
                            var attachmentValidationResult = await ValidateAttachmentAsync(lessonDto.Attachment);
                            if (attachmentValidationResult != null)
                            {
                                throw new ApiException(attachmentValidationResult);
                            }

                            var attachment = _mapper.Map<Attachment>(lessonDto.Attachment);
                            await _attachmentService.AddAsync(attachment);
                        }

                        if (lessonDto.Video != null)
                        {
                            lessonDto.Video.LessonId = createdLesson.Id;
                            var videoValidationResult = await ValidateVideoAsync(lessonDto.Video);
                            if (videoValidationResult != null)
                            {
                                throw new ApiException(videoValidationResult);
                            }

                            var video = _mapper.Map<Video>(lessonDto.Video);

                            await _videoService.AddAsync(video);
                        }

                        if (lessonDto.Article != null)
                        {
                            lessonDto.Article.LessonId = createdLesson.Id;
                            var articleValidationResult = await ValidateArticleAsync(lessonDto.Article);
                            if (articleValidationResult != null)
                            {
                                throw new ApiException(articleValidationResult);
                            }

                            var article = _mapper.Map<Article>(lessonDto.Article);

                            await _articleService.AddAsync(article);
                        }

                        if (lessonDto.Quiz != null)
                        {
                            lessonDto.Quiz.LessonId = createdLesson.Id;
                            var quizValidationResult = await ValidateQuizAsync(lessonDto.Quiz);
                            if (quizValidationResult != null)
                            {
                                throw new ApiException(quizValidationResult);
                            }

                            var quiz = _mapper.Map<Quiz>(lessonDto.Quiz);

                            var createdQuiz = await _quizService.AddAsync(quiz);

                            foreach (var questionDto in lessonDto.Quiz.Questions)
                            {
                                questionDto.QuizId = createdQuiz.Id;
                                var questionValidationResult = await ValidateQuestionAsync(questionDto);
                                if (questionValidationResult != null)
                                {
                                    throw new ApiException(questionValidationResult);
                                }

                                var question = _mapper.Map<Question>(questionDto);
                                await _questionService.AddAsync(question);
                            }
                        }
                    }
                }

                transaction.Complete();
            }
            catch
            {
                transaction.Dispose();
                throw;
            }
        }
        private async Task<string?> ValidateChapterAsync(CreateChapterDto chapterDto)
        {

            var validationError = await _chapterValidatorService.ValidateAsync(chapterDto, true);
            return validationError;

        }

        private async Task<string?> ValidateLessonAsync(CreateLessonDto lessonDto)
        {
            var validationError = await _lessonValidatorService.ValidateAsync(lessonDto, true);
            return validationError;

        }

        private async Task<string?> ValidateAttachmentAsync(AttachmentCreateDto attachmentDto)
        {
            var validationError = await _attachmentValidatorService.ValidateAsync(attachmentDto, true);
            return validationError;
        }

        private async Task<string?> ValidateVideoAsync(VideoCreateDto videoDto)
        {
            var validationError = await _videoValidatorService.ValidateAsync(videoDto, true);
            return validationError;
        }

        private async Task<string?> ValidateArticleAsync(ArticleCreateDto articleDto)
        {
            var validationError = await _articleValidatorService.ValidateAsync(articleDto, true);
            return validationError;
        }

        private async Task<string?> ValidateQuizAsync(QuizCreateDto quizDto)
        {
            var validationError = await _quizValidatorService.ValidateAsync(quizDto, true);
            return validationError;

        }

        private async Task<string?> ValidateQuestionAsync(QuestionCreateDto questionDto)
        {
            var validationError = await _questionValidatorService.ValidateAsync(questionDto, true);
            return validationError;

        }

    }
}
