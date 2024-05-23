using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        // private readonly IGenericRepository<Product> _productRepo;
        // private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        //         IGenericRepository<Product> productRepo,
        //         IGenericRepository<DeliveryMethod> dmRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
            // _dmRepo = dmRepo;
            // _productRepo = productRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                //var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            //var deliveryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // check to see if order exists
            //var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            //var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            //var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            //     if (order != null)
            //     {
            //         order.ShipToAddress = shippingAddress;
            //         order.DeliveryMethod = deliveryMethod;
            //         order.Subtotal = subtotal;
            //         _unitOfWork.Repository<Order>().Update(order);
            //     }
            //     else
            //     {
            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);
            //     }

            // save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            //delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            // return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpec(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpec(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}