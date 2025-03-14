using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        [Authorize(Roles = "Admin, Customer")]
        [HttpGet("get-cart-by-id/{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _cartRepository.GetCartByID(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }


        [Authorize(Roles = "Customer")]
        [HttpPost("add-cart")]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            await _cartRepository.CreateCart(cart);
            return CreatedAtAction(nameof(GetCart), new { id = cart.CartID }, cart);
        }


        [Authorize(Roles = "Customer")]
        [HttpPut("update-cart/{id}")]
        public async Task<IActionResult> UpdateCart(int id, Cart cart)
        {
            if (id != cart.CartID)
            {
                return BadRequest();
            }

            await _cartRepository.UpdateCart(cart);
            return NoContent();
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete("delete-cart/{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _cartRepository.GetCartByID(id);
            if (cart == null)
            {
                return NotFound();
            }

            await _cartRepository.DeleteCart(id);
            return NoContent();
        }

        //[Authorize(Roles = "Customer")]
        //[HttpPost("checkout/{cartId}")]
        //public bool Checkout(int cartId)
        //{
        //    var OrderCreated = _cartRepository.CheckOutCart(cartId);
        //    if (OrderCreated)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        [Authorize(Roles = "Customer")]
        [HttpPost("add-item-to-cart/{userId}")]
        public IActionResult AddItemToCart(int userId, CartItem newItem)
        {
            var CheckIfAdded = _cartRepository.AddItemsToCart(userId, newItem);
            if (CheckIfAdded)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
