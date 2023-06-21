using System.ComponentModel.DataAnnotations;

namespace Share.Models
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; } // Khóa chính
        [Required(ErrorMessage ="Tên sản phẩm không được để trống")]
        public string Name { get; set; } // Tên sản phẩm
        public string ?Slug { get; set; } // slug 
        public string ?Image { get; set; } // Hình ảnh
        public bool? IsFreeship { get; set; } // có freeship không
        public string ?ListImage { get; set; } // Nhiều sản phẩm
        public decimal? Price { get; set; } // Gía tiền
        public decimal? PromotionPrice { get; set;} // Gía khuyến mãi
        public int? Quantity { get; set; } // Số lượng
        public DateTime ?Hot { get; set; } // Sản phẩm hot từ ngày nào tới ngày nào
        public string? Content { get; set; } // Chi tiết ngắn
        public string? Description { get; set; } // Chi tiết sản phẩm
        public int? ViewCount { get; set; } // lượt xem
        public string? MetaKeyword { get; set; } // Hỗ trợ SEO
        public string? MetaDescription { get; set; } // Hỗ trợ SEO
        public int? CreateBy { get; set; } // taọ mới bởi ai 1 : SPAdmin 2 : manager 
        public int? UpdateBy { get; set; } // bởi ai 1 : SPAdmin 2 : manager 
        public DateTime CreateDate { get; set; } = DateTime.Now; // Ngày tạo
        public DateTime UpdateDate { get; set; } // Ngày cập nhật 
        public ProductCategory ?ProductCategory { get; set; } // danh mục sản phẩm
        public Guid? CateID { get; set; } // khóa ngoại
        public Supplier? Supplier { get; set; } // Nhà cung cấp
        public Guid? Supplierid { get; set;} // Khóa ngoại nhà cung câps
        public Brand? Brand { get; set; } // Thương hiệu
        public int? BrandID { get; set; } // Khóa ngoại thương hiệu


    }
}
