using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    public class PostCommentVM
    {
       
        public string? PostCommentName { get; set; } // Tên người bình luận
        public Guid CustomerId { get; set; } // Khóa ngoại
        [EmailAddress(ErrorMessage = "Email không hợp lệ !")]
        public string? Email { get; set; } // Emaill
        public string? Detail { get; set; } // Nội dung bình luận
        //public DateTime CreateDate { get; set; } // Ngày tạo
        //public DateTime UpdateDate { get; set; } // Ngày cập nhật
        //public PostVM? Post { get; set; } // Bài viết
        //public CustomerVM? Customer { get; set; } // Khách hàng
        public Guid PostID { get; set; } // Khóa ngoại
    }
}
