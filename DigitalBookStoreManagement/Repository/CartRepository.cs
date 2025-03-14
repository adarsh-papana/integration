using DigitalBookStoreManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly UserContext _context;

        public CartRepository(UserContext context)
        {
            _context = context;
        }



        public async Task<Cart> GetCartByID(int id)
        {
            return await _context.Carts.Include(c => c.CartItems).FirstOrDefaultAsync(c => c.CartID == id);
        }

        public async Task CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public bool AddItemsToCart(int userId, CartItem newItem)
        {
            var CheckCartExists = _context.Carts.Include(ci => ci.CartItems).FirstOrDefault(c => c.UserID == userId);
            if (CheckCartExists == null)
            {
                var newCart = new Cart { UserID = userId, CreatedDate = DateTime.Now };
                _context.Carts.Add(newCart);
                _context.SaveChanges();



            }

            var cartItemToAdd = CheckCartExists.CartItems.FirstOrDefault(i => i.BookID == newItem.BookID);
            if (cartItemToAdd != null)
            {
                cartItemToAdd.Quantity = newItem.Quantity;

            }
            cartItemToAdd = new CartItem
            {
                CartID = newItem.CartID,
                BookID = newItem.BookID,
                Quantity = newItem.Quantity,
                Price = newItem.Price,
                TotalAmount = newItem.TotalAmount,


            };
            _context.CartItems.Add(cartItemToAdd);
            return _context.SaveChanges() > 0;


        }

        public async Task UpdateCart(Cart cart)
        {
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        //public bool CheckOutCart(int cartId)
        //{
        //    var cart = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.CartID == cartId);
        //    if (cart == null)
        //    {
        //        return false;
        //    }
        //    Order NewOrder = new Order
        //    {
        //        UserID = cart.UserID,
        //        OrderStatus = "Pending",
        //        OrderDate = DateTime.Now,
        //        PaymentStatus = "Not Paid",
        //        OrderItems = cart.CartItems.Select(ci => new OrderItem
        //        {
        //            BookID = ci.BookID,
        //            Price = ci.Price,
        //            Quantity = ci.Quantity,
        //            TotalAmount = ci.Price * ci.Quantity
        //        }).ToList()
        //    };
        //    _context.Orders.Add(NewOrder);
        //    _context.Carts.Remove(cart);
        //    _context.SaveChanges();
        //    return true;
        //}
    }
}
