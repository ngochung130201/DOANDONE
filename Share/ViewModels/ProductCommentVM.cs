using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    public class ProductCommentVM
    {
        [Key]
      
        public string? ProductCommentName { get; set; } // Tên người bình luận
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; } // Emaill
        public string? Detail { get; set; } // Nội dung bình luận
      
        //public CustomerVM? Customer { get; set; } // Khách hàng
        //public ProductVM? Product { get; set; } // Bài viết
        public Guid CustomerId { get; set; } // Khóa ngoại
        public Guid ProductId { get; set; } // Khóa ngoại

    }
}
