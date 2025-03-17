namespace DigitalBookstoreManagement.Models
{
    public class InventoryDTO
    {
        public int InventoryID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
        public int NotifyLimit { get; set; }
    }
}
