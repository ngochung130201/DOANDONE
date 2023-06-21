using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Models
{
  
    public class Contact
    {
        [Key]
        public int ContactId { get; set; } // khóa chính
        [Required(ErrorMessage ="Bắt buột nhập")]
        public string? Name { get; set; } // tên liên hệ
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; } // email
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không hợp lệ !.")]
        public string? Phone { get; set; } // số điện thoại
        public string? Address { get; set; } // địa chỉ
        public string? Subject { get; set; } // Tiêu đề 
        public string? Body { get; set; }  // Nội dung
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
