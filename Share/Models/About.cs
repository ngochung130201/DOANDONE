using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.Models
{
     // Giới thiệu về công ty
    public class About
    {
        
        [Key]
        public int Id { get; set; }
        public string ?Tilte { get; set; } // Tiêu đề
        public string Description { get; set; } = string.Empty; // Giới thiệu về công ty
        public string ?Facebook { get; set; }
        public string ?Zalo { get; set; }
        public string ?MetaKeyword { get; set; } // Hô trợ SEO
        public string ?MetaDescription { get; set; } // Hô trợ SEO
        public int CreateBy { get; set; } // taọ mới bởi ai 1 : SPAdmin 2 : manager 
        public int UpdateBy { get; set; } // cập nhập bởi ai 1 : SPAdmin 2 : manager 
        public DateTime CreateDate { get; set; } = DateTime.Now; // ngày tạo
        public DateTime UpdateDate { get; set; } // ngày cập nhật


    }
}
