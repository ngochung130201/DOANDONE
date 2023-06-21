using Share.Models;
using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    public class PostVM
    {
        [Key]
        public Guid PostID { get; set; } // Khóa chính
        [Required]
        public string? Name { get; set; } // title
        public string? Slug { get; set; } // slug 
        public string? Image { get; set; } // Hình ảnh
        public string? Content { get; set; } // chi tiết ngắn
        public string? Description { get; set; } // Chi tiết bài viết
        public int ViewCount { get; set; } // Số lương người xem tự động tăng
        public string? MetaKeyword { get; set; } // Hỗ trợ SEO
        public string? MetaDescription { get; set; } // Hỗ trợ SEO
        public int? CreateBy { get; set; } // taọ mới bởi ai 1 : SPAdmin 2 : manager 
        public int? UpdateBy { get; set; } // Cập mới bởi ai 1 : SPAdmin 2 : manager 
        public DateTime? CreateDate { get; set; } // ngày tạo
        public DateTime? UpdateDate { get; set; } // ngày cập nhật
        //public PostCategory? PostCategory { get; set; }
        public Guid CateID { get; set; }
    }
}
