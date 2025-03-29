using NoahStore.Core.Entities;

namespace NoahStore.Core.Specifications
{
    public class ProductWithImagesAndCategory: BaseSpecification<Product>
    {
        public ProductWithImagesAndCategory(ProductSpecsParams productSpecs) 
            :base(p=> (
            (!productSpecs.CategoryId.HasValue || p.CategoryId == productSpecs.CategoryId.Value)) &&
           (string.IsNullOrEmpty(productSpecs.Search) || p.Name.ToLower().Contains(productSpecs.Search))
            )
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(productSpecs.Sort))
            {
                switch (productSpecs.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "priceDsc":
                        AddOrderByDesc(p=>p.Price);
                        break;
                    case "name":
                        AddOrderBy(P => P.Name);
                        break;
                    default:
                        AddOrderBy(p=>p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);

            }

            ApplyPagaination(productSpecs.PageSize * (productSpecs.PageIndex -1), productSpecs.PageSize);
        }
        public ProductWithImagesAndCategory(int id):base(p=>p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Images);
        }
    }
}
