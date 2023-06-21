using Share.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Share.Models
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; } // khóa chính
        public DateTime OrderDate { get; set; } // ngày đặt
        public Status IsStatus { get; set; } // 1: chờ xác nhận, 2: Đang giao hàng, 3:giao hàng thành công,4 : trả hàng
        public Customer? Customer { get; set; }
        public Guid CustomerID { get; set; }
        public Guid MemberId { get; set; } // Ai là người duyệt đơn hàng
        public UserDentity UserDentity { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
      
        public decimal TotalAmount { get; set; } // tong sso tien
     

    }
}
