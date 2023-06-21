using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.Models
{
   
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; } // Khóa chính
        public string? CustomerId { get; set; } // khoá phu
        public  Customer Customer { get; set; }
        public UserDentity Member { get; set; }
        public Guid MemberId { get; set; }
        public string? Content { get; set; } // Nội dung feedback
        public DateTime CreateDate { get; set; } = DateTime.Now; // ngày tạo
        public DateTime UpdateDate { get; set; } // ngày cập nhật

    }
}
