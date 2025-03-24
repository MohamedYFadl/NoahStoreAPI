using NoahStore.Core.Entities;

namespace NoahStore.Core.Specifications
{
    public class ProductWithImagesAndCategory: BaseSpecification<Product>
    {
        public ProductWithImagesAndCategory(string? sort):base()
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
