using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ViewModels
{
  
    public class ContactVM
    {
        [Key]
   
        [Required(ErrorMessage ="Tên liên hệ không đươc để trống")]
        public string? Name { get; set; } // tên liên hệ
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; } // email
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ !.")]
        public string? Phone { get; set; } // số điện thoại
        [MaxLength(100,ErrorMessage = "Địa chỉ không được dài hơn 100 ký tự")]
        public string? Address { get; set; } // địa chỉ
        [MaxLength(50, ErrorMessage = "Địa chỉ không được dài hơn 50 ký tự")]
        public string? Subject { get; set; } // Tiêu đề 
        [MaxLength(500, ErrorMessage = "Địa chỉ không được dài hơn 500 ký tự")]
        public string? Body { get; set; }  // Nội dung
    
    }
}
