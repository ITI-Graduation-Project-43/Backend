using MindMission.Application.DTOs.CourseChapters;


namespace MindMission.Application.Interfaces.Services
{
    public interface IChapterLessonsService
    {
        public Task AddChapters(List<CreateChapterDto> chapterDtos);

    }
}
