namespace MindMission.Domain.Models.Base
{
    /// <summary>
    /// Represents the base entity class with common properties for entity creation and modification.
    /// </summary>
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
