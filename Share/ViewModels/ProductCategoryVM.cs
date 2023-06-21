
using System.ComponentModel.DataAnnotations;

namespace Share.ViewModels
{
    // Bang danh muc san pham
    public class ProductCategoryVM
    {
      
        [Required]
        [MaxLength(100)]
        public string? ProductCateName { get; set; } // Tên danh mục
    
        public int? Sort { get; set; } // Thứ tự ưu tiên
        public int? ParentID { get; set; } // Danh mục cha
        public string? MetaKeyword { get; set; } // Hỗ trợ SEO
        public string? MetaDescription { get; set; }// Hỗ trợ SEO
      
   

    }
}
