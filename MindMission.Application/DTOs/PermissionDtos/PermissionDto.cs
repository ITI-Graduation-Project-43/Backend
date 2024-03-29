﻿namespace MindMission.Application.DTOs.PermissionDtos
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> AdminNames { get; set; } = new List<string>();

    }
}
