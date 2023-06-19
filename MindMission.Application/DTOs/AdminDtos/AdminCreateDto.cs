using MindMission.Application.CustomValidation.DataAnnotation;
using MindMission.Application.DTOs.Base;



namespace MindMission.Application.DTOs.AdminDtos
{
    public class AdminCreateDto : IDtoWithId<int>
    {
        public int Id { get; set; }
        [RequiredField("First Name")]
        [RangeValueAttribute(2, 50)]
        [Alphabetic]
        public string FirstName { get; set; } = string.Empty;

        [RequiredField("Last Name")]
        [RangeValueAttribute(2, 50)]
        [Alphabetic]
        public string LastName { get; set; } = string.Empty;

        [RequiredField("Email")]
        [CustomEmailAddress]
        public string Email { get; set; } = string.Empty;

        [ImageFileAttribute]
        public string ProfilePicture { get; set; } = string.Empty;

        [RequiredField("Password")]
        [StrongPasswordAttribute]
        public string PasswordHash { get; set; } = string.Empty;

    }
}
