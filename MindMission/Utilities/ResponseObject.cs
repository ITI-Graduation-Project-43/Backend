namespace MindMission.API.Utilities
{
    public class ResponseObjectX<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }

        public void ReturnedResponse(bool _Success, string _Message, IEnumerable<T> _Items, int _PageNumber, int _ItemsPerPage, int _TotalPages)
        {
            Success = _Success;
            Message = _Message;
            Items = _Items;
            PageNumber = _PageNumber;
            ItemsPerPage = _ItemsPerPage;
            TotalPages = _TotalPages;
        }
    }
}