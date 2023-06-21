using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Models
{
  
    // Bảng menu 
    public class Menu
    {
         [Key]
         public int Id { get; set; }
         [Required(ErrorMessage = "Trường này bắt buộc")]
         [MaxLength(20,ErrorMessage ="Không được dài quá 20 kí tự")]
          public string? Name { get; set; } // tên menu
       
          public string? Link { get; set; } // đường dần khi click vào menu (slug)
          public bool IsStatus { get; set; } = true; // trạng thái (true : hiện , false : ẩn)

    }
}
