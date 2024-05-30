using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderingSpec : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrderingSpec(string email, OrderSpecParams orderSpec) 
        : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
            ApplyPaging(orderSpec.PageSize, orderSpec.PageSize * (orderSpec.PageIndex -1));            
        }

        public OrdersWithItemsAndOrderingSpec(int id, string email)
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }

    }
}