using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.ViewModels
{
   
    public class FeedbackVM
    {
       
        public string? CustomerId { get; set; } // khoá phu
        public  CustomerVM Customer { get; set; }
        public MemberVM Member { get; set; }
        public Guid MemberId { get; set; }
        public string? Content { get; set; } // Nội dung feedback
        public DateTime CreateDate { get; set; } = DateTime.Now; // ngày tạo
        public DateTime UpdateDate { get; set; } // ngày cập nhật

    }
}
