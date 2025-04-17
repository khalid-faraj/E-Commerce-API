namespace API.DTOs
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PicUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
