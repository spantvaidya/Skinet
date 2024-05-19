using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task <CustomerBasket> GetBasketAsync(string BasketId);
        Task <CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool>DeleteBasketAsync(string BasketId);
    }
}