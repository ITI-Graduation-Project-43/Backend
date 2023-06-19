﻿using System.ComponentModel.DataAnnotations;


namespace MindMission.Application.DTOs.Admin
{
    public class AdminCreateDto
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name should contain only alphabetic characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Name should contain only alphabetic characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^.+\.(png|jpg)$", ErrorMessage = "Profile picture should be a .png or .jpg file.")]
        public string ProfilePicture { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password should be a strong password.")]
        public string PasswordHash { get; set; } = string.Empty;

        public List<int> AdminPermissions { get; set; } = new List<int>();
    }
}