using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.ViewModels
{
     // Giới thiệu về công ty
    public class AboutVM
    {
        
        public string ?Tilte { get; set; } // Tiêu đề
        public string Description { get; set; } = string.Empty; // Giới thiệu về công ty
        public string ?Facebook { get; set; }
        public string ?Zalo { get; set; }
        public string ?MetaKeyword { get; set; } // Hô trợ SEO
        public string ?MetaDescription { get; set; } // Hô trợ SEO
        public int CreateBy { get; set; } // taọ mới bởi ai 1 : SPAdmin 2 : manager 
        public int UpdateBy { get; set; } // cập nhập bởi ai 1 : SPAdmin 2 : manager 



    }
}
