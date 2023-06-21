using Share.Helpers;
using Share.Models;
using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    public class OrderVM
    {
        //o.OrderID ,o.OrderDate ,o.CustomerID ,o.IsStatus  ,o.MemberId ,c.FullName ,c.Phone ,c.Email,c.Address
       [Key]
        public Guid OrderID { get; set; } // khóa chính
        public DateTime OrderDate { get; set; } // ngày đặt
        public Status IsStatus { get; set; } // 1: chờ xác nhận, 2: Đang giao hàng, 3:giao hàng thành công,4 : trả hàng
      public Customer Customer { get; set; }
        public Guid CustomerID { get; set; }
        public Guid MemberId { get; set; } // Ai là người duyệt đơn hàng
                                           //public MemberVM Member { get; set; }
                                           //public CustomerVM? Customer { get; set; }
                                           //public ProductVM? Product { get; set; }
                                           
      public string FullName { get; set; }
      public string Phone { get; set; }
      public string Email { get; set; }
      public string Address { get; set; }
        public string TotalAmount { get; set; }
        public string MemberName { get; set; }

    }
}
