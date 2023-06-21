using System.ComponentModel.DataAnnotations;

namespace Share.Models
{
    public class ProductComment
    {
        [Key]
        public Guid CommentID { get; set; } //  Khóa chỉnh bình luận bài viết
        public string? ProductCommentName { get; set; } // Tên người bình luận
        public Customer? Customer { get; set; } // Khách hàng
        public Guid CustomerId { get; set; } // Khóa ngoại
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; } // Emaill
        public string? Detail { get; set; } // Nội dung bình luận
        public DateTime CreateDate { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime UpdateDate { get; set; } // Ngày cập nhật
        public Product? Product { get; set; } // Bài viết
        public Guid ProductId { get; set; } // Khóa ngoại

    }
}
