using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ViewModels
{
   // Thương hiệu
    public class BrandVM
    {
        [Key]
      
        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        [MaxLength(100,ErrorMessage = "Tên thương hiệu dưới 100 kí tự")]
        public string? BrandName { get; set; } // Tên thương hiệu
        public string? Image { get; set; } // Hình ảnh thương hiệu
    }
}
