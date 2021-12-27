using System;
using System.Collections.Generic;

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
        public bool HasNext => CurrentPage < TotalPages;
        public int[] PageNumbers => Pagination(CurrentPage, TotalPages);
        public static int[] Pagination(int current, int last)
        {
            int delta = 1;
            int left = current - delta;
            int right = current + delta + 1;
            var range = new List<int>();
            var rangeWithDots = new List<int>();
            int? l = null;

            for (var i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || i >= left && i < right)
                {
                    range.Add(i);
                }
            }

            foreach (var i in range)
            {
                if (l != null)
                {
                    if (i - l == 2)
                    {
                        rangeWithDots.Add(l.Value + 1);
                    }
                    else if (i - l != 1)
                    {
                        rangeWithDots.Add(-1);
                    }
                }
                rangeWithDots.Add(i);
                l = i;
            }

            return rangeWithDots.ToArray();
        }
    }
}
