using NoahStore.Core.Entities;
using NoahStore.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahStore.Core.Specifications
{
    public class FilteredProductsForCountSpecs:BaseSpecification<Product>
    {
        public FilteredProductsForCountSpecs(ProductSpecsParams specsParams) : base(p => (
            (!specsParams.CategoryId.HasValue || p.CategoryId == specsParams.CategoryId.Value)) &&
           (string.IsNullOrEmpty(specsParams.Search) || p.Name.ToLower().Contains(specsParams.Search))
            )
        {
            
        }
    }
}
