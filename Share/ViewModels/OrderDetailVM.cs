using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    public class OrderDetailVM
    {
        [Key]
        public Guid OrderDetailId { get; set; } // Khoá chính chi tiết đặt hàng
        public Guid OrderId { get; set; } // Khóa ngoại tới bảng Order
        public Guid ProductID { get; set; } // Khóa ngoại bảng Product
        public virtual ProductVM Product { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool? IsFreeShip { get; set; }
     
    }
}
