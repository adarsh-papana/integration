using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByID(int id);
        Task CreateCart(Cart cart);
        bool AddItemsToCart(int userId, CartItem newItem);
        Task UpdateCart(Cart cart);
        Task DeleteCart(int id);

        //bool CheckOutCart(int cartId);
    }
}
