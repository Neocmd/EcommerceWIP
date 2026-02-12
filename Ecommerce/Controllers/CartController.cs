using Ecommerce.Exceptions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> AddItem(int bookId, int qty = 1, int redirect = 0)
        {
            try
            {
                var cartCount = await _cartService.AddItemAsync(bookId, qty);
                if (redirect == 0)
                {
                    return Ok(cartCount);
                }

                return RedirectToAction(nameof(GetUserCart));
            }
            catch (AppException ex)
            {
                if (redirect == 1)
                {
                    TempData["CartError"] = ex.Message;
                    return RedirectToAction(nameof(GetUserCart));
                }

                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            try
            {
                await _cartService.RemoveItemAsync(bookId);
            }
            catch (AppException ex)
            {
                TempData["CartError"] = ex.Message;
            }

            return RedirectToAction(nameof(GetUserCart));
        }

        public async Task<IActionResult> GetUserCart()
        {
            try
            {
                var cart = await _cartService.GetUserCartAsync();
                return View(cart);
            }
            catch (AppException ex)
            {
                TempData["CartError"] = ex.Message;
                return View(new ShoppingCart());
            }
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            try
            {
                var cartItem = await _cartService.GetCartItemCountAsync();
                return Ok(cartItem);
            }
            catch (AppException ex)
            {
                return StatusCode(ex.StatusCode, new { message = ex.Message });
            }
        }

        public async Task<IActionResult> Checkout()
        {
            try
            {
                await _cartService.CheckoutAsync();
                TempData["CartSuccess"] = "Checkout completed successfully.";
                return RedirectToAction("Index", "Home");
            }
            catch (AppException ex)
            {
                TempData["CartError"] = ex.Message;
                return RedirectToAction(nameof(GetUserCart));
            }
        }
    }
}
