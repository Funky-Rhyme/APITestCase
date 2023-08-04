namespace SystemsOfControlAPI.Entities.Services
{
    public class PaginationParams
    {
        private const int _maxItemsPerPage = 50;
        private int itemsPerPage;

        public int Page { get; set; } = 1;
        public int ItemPerPage
        {
            get => itemsPerPage;
            set => itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}
