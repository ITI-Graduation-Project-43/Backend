using MindMission.Application.Responses;


namespace MindMission.Application.Factories
{
    public static class ResponseObjectFactory
    {
        public static ResponseObject<T> CreateResponseObject<T>(bool success, string message, List<T> items, int pageNumber, int itemsPerPage)
        {
            return new ResponseObject<T>
            {
                Success = success,
                Message = message,
                Items = items,
                PageNumber = pageNumber,
                ItemsPerPage = itemsPerPage,
                TotalPages = CalculateTotalPages(items.Count, itemsPerPage)
            };
        }

        private static int CalculateTotalPages(int itemCount, int itemsPerPage)
        {
            return (itemCount + itemsPerPage - 1) / itemsPerPage;
        }
    }
}
