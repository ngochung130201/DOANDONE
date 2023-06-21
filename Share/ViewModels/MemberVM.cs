using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    public class MemberVM
    {
        [Key]
        public Guid ID { set; get; }
        public string? LastName { set; get; } // họ
        public string? FirstName { set; get; } // tên
        public string? Email { set; get; } // Email
        public string? Telephone { set; get; } // số điện thoại
        public string? About { set; get; } // giới thiệu cá nhân
        public string? Password { set; get; } // mật khẩu
        public bool RememberMe { set; get; } // có nhớ mật khẩu không
        public DateTime DateCreated { set; get; } // ngày tạo
        public string? Avatar { set; get; } // ảnh đại diện
        public DateTime? Birthday { set; get; } // ngày sinh nhật
        
    }
}
