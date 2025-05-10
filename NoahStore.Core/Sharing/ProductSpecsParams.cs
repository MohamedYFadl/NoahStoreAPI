using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Core.Sharing
{
    public class ProductSpecsParams
    {
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int Count { get; set; }
        private const int MaxPageSize = 10;
        private int pageSize = 5;


        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }
    }
}
