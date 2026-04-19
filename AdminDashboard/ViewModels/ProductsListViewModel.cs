using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminDashboard.ViewModels
{
    public class ProductsListViewModel
    {
        public SelectList Categories { get; set; }
        public string? Term { get; set; }
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IReadOnlyList<ProductsViewModel> Products { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
