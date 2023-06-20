using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class WishlistDto : IDtoWithId<int>
    {
        public string StudentName { get; set; } = string.Empty;
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public decimal CoursePrice { get; set; }
        public string CourseImageUrl { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public decimal? CourseAvgReview { get; set; }
        public string CourseDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}