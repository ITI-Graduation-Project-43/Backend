using MindMission.Application.DTOs.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.DTOs
{
    public class InstructorDto: IDtoWithId
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }= string.Empty;

        public string Bio { get;set; }=string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string Title { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NoOfCources { get; set; }
        public int NoOfStudents { get; set; }
        public int NoOfCourses { get; set; }
        public double AvgRating { get; set; }
        public int NoOfRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Dictionary<string,string> accounts { get; set; } = new Dictionary<string,string>();
        int IDtoWithId.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
