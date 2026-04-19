using NoahStore.Core.Entities;

namespace NoahStore.Core.Specifications
{
    public class ProductsSpecs:BaseSpecification<Product>
    {
        public ProductsSpecs():base()
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Images);
        }
    }
}
