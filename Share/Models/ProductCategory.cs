
using System.ComponentModel.DataAnnotations;

namespace Share.Models
{
    // Bang danh muc san pham
    public class ProductCategory
    {
        [Key]
        public Guid CateID { get; set; } // Khóa chính
        [Required]
        [MaxLength(100)]
        public string? ProductCateName { get; set; } // Tên danh mục
        public string? Slug { get; set; } // Slug
        public int? Sort { get; set; } // Thứ tự ưu tiên
        public int? ParentID { get; set; } // Danh mục cha
        public string? MetaKeyword { get; set; } // Hỗ trợ SEO
        public string? MetaDescription { get; set; }// Hỗ trợ SEO
        public DateTime CreateDate { get; set; } = DateTime.Now; // Ngàu tạo
        public DateTime UpdateDate { get; set; } // Ngày cập nhật
   

    }
}
