using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrderFilterForCountSpec(OrderSpecParams specParams)
    : BaseSpecification<Order>(o => String.IsNullOrEmpty(specParams.Search) || o.Id == 0)
    {
    }
}