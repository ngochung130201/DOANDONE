using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Models
{

    public class Customer
    {
        [Key]
        public Guid CustomerId { set; get; } // Khóa chính
        public string? FullName { set; get; } // tên khách hàng
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ !.")]
        public string? Phone { set; get; } // số điện thoại khách hàng
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { set; get; } // email khách hàng
        public string? Address { set; get; } // Địa chủ
    }
}
