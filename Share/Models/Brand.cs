using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Models
{
   // Thương hiệu
    public class Brand
    {
        [Key]
        public int BrandID { get; set; } // Khóa chính
        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        public string? BrandName { get; set; } // Tên thương hiệu
        public string? Image { get; set; } // Hình ảnh thương hiệu
    }
}
