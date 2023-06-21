using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ViewModels
{
    
    public class SliderVM
    {
      
        public string? Title { get; set; } // tiêu đề
        public string? Image { get; set; } // hình ảnh slider
        public string? Link { get; set; } // slug 
        public bool? IsStatus { get; set; } = true; // trạng thái (true : hiện , false : ẩn)

    }
}
