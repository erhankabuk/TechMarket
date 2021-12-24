using System;

namespace Web.Models
{
    public class PaginationInfoViewModel
    {
        public PaginationInfoViewModel()
        {

        }
        public PaginationInfoViewModel(int totalItems, int currentPage, int itemsPerPage, int itemsOnPage)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            ItemsOnPage = itemsOnPage;
        }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int ItemsOnPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < ItemsPerPage;
    }
}
