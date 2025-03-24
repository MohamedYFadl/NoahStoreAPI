using NoahStore.Core.Entities;

namespace NoahStore.Core.Specifications
{
    public class ProductWithImagesAndCategory: BaseSpecification<Product>
    {
        public ProductWithImagesAndCategory():base()
        {
            AddIncludes();
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
