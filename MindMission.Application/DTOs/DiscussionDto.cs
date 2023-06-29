using MindMission.Application.DTOs.Base;

namespace MindMission.Application.DTOs
{
    public class DiscussionDto : IDtoWithId<int>, IEquatable<DiscussionDto>
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int? ParentDiscussionId { get; set; } = null;
        public string Content { get; set; } = string.Empty;
        public int Upvotes { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public string Username { get; set; } = string.Empty;
        public string ParentContent { get; set; } = string.Empty;



        public bool Equals(DiscussionDto? other)
        {
            if (other is null) return false;
            return Id == other.Id && LessonId == other.LessonId && UserId == other.UserId && ParentDiscussionId == other.ParentDiscussionId && Content == other.Content && Upvotes == other.Upvotes;
        }
        public override bool Equals(object? obj)
        { return Equals(obj as DiscussionDto); }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Content);
        }
    }
}