using Ecommerce.Exceptions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IOrderService _orderService;

        public UserOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> UserOrders()
        {
            try
            {
                var orders = await _orderService.GetUserOrdersAsync();
                return View(orders);
            }
            catch (AppException ex)
            {
                TempData["OrderError"] = ex.Message;
                return View(Enumerable.Empty<Order>());
            }
        }
    }
}
