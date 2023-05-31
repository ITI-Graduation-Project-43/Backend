namespace MindMission.Application.DTOs.Base
{
    public interface IDtoWithId<Type>
    {
        Type Id { get; set; }
    }
}