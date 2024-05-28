using Application.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobileAPI.Controllers
{
    
    public class CartController : BaseController
    {
        private readonly IProductService _productService;
        public CartController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(List<Guid> productId)
        {
            var isAdded = await _productService.AddToCart(productId);
            if (isAdded)
            {
                return Ok();
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet]
        public IActionResult ViewCartData()
        {
            var cartData = _productService.ViewCart();
            return Ok(cartData);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteItemInCart(Guid itemId)
        {
            bool isRemove= _productService.RemoveFromCart(itemId);
            if (isRemove)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
