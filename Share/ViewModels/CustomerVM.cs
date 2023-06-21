using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ViewModels
{

    public class CustomerVM
    {

        public string? FullName { set; get; } // tên khách hàng
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ !.")]
        public string? Phone { set; get; } // số điện thoại khách hàng
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { set; get; } // email khách hàng
        [MaxLength(100,ErrorMessage = "Địa chỉ không được dài hơn 100 ký tự")]
        public string? Address { set; get; } // Địa chủ
    }
}
