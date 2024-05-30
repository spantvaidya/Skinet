using API.DTOs;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController(IOrderService orderService, IMapper mapper, 
    IGenericRepository<Order> orderRepo) : BaseApiController
    {
        private readonly IOrderService _orderService = orderService;

        private readonly IGenericRepository<Order> _orderRepo = orderRepo;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<OrderToReturnDto>>> GetOrdersForUser
        ([FromQuery] OrderSpecParams orderParams)
        {
            var email = User.RetrieveEmailFromPrincipal();
            var countSpec = new OrderFilterForCountSpec(orderParams);//for pagination
             var totalorders = await _orderRepo.CountAsync(countSpec);

            var orders = await _orderService.GetOrdersForUserAsync(email,orderParams);
            var ordersData = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);

            return Ok(new Pagination<OrderToReturnDto>(orderParams.PageIndex, 
            orderParams.PageSize, totalorders, ordersData));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<OrderToReturnDto>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}