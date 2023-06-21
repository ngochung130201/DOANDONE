using System.ComponentModel.DataAnnotations;

namespace Share.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailId { get; set; } // Khoá chính chi tiết đặt hàng
        public Order Order { get; set; }
        public Guid OrderId { get; set; } // Khóa ngoại tới bảng Order
        public Guid ProductID { get; set; } // Khóa ngoại bảng Product
        public virtual Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
 
    
    }
}
