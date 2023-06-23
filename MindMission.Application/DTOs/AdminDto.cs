using MindMission.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace MindMission.Application.DTOs
{
    public class AdminDto : IDtoWithId<string>
    {

        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name should contain only alphabetic characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name should contain only alphabetic characters.")]
        public string LastName { get; set; } = string.Empty;

        [RegularExpression(@"^.+\.(png|jpg)$", ErrorMessage = "Profile picture should be a .png or .jpg file.")]
        public string ProfilePicture { get; set; } = string.Empty;
        public List<int> AdminPermissions { get; set; } = new List<int>();

    }


}