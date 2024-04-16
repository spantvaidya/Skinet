
using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecwithBrandAndType : BaseSpecification<Product>
    {
        public ProductSpecwithBrandAndType()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductSpecwithBrandAndType(int id) : base(x => x.id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}