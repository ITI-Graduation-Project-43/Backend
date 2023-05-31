namespace MindMission.Application.Responses
{
    public class ResponseObject<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
    }
}