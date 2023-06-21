using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.ViewModels
{
   
    public class PostCategoryVM
    {
        //[Key]
        //public Guid CateID { get; set; } // Khóa chính
        [Required]
        [MaxLength(100)]
        public string? PostCateName { get; set; } // Tiêu đề 
        public string? Slug { get; set; } // Slug
        public int? Sort { get; set; } // Thứ tự ưu tiên
        public int? ParentID { get; set; } // Danh mục cha
        public string? MetaKeyword { get; set; } // Hỗ trợ SEO
        public string? MetaDescription { get; set; } // Hỗ trợ SEO
        public int? CreateBy { get; set; } // taọ mới bởi ai 1 : SPAdmin 2 : manager 
        public int? UpdateBy { get; set; } // taọ mới bởi ai 1 : SPAdmin 2 : manager 
        //public DateTime CreateDate { get; set; } // Ngày tạo
        //public DateTime UpdateDate { get; set; } // Ngày cập nhật
     //   public PostVM? Post { get; set; } // Bài viết
       // public Guid PostID { get; set; } // Khóa ngoại bài viết
    }
}
