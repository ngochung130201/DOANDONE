using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Share.Models;

namespace Share.ViewModels
{
    public class StatisticalVM
    {
        // Tong so don hang
        public int TotalOrder { get; set; }
        // Tong so don hang chua  xac nhan
        public int TotalOrderUnconfirmed { get; set; }
        // Tong tien doanh thu
        public decimal Revenue { get; set; }
        // Tong so luong khach hang
        public int TotalCustomer { get; set; }
        public int TotalComment { get; set; }
      public List<ProductVM>? ProductOrderViewCount { get; set; } // Update the property type to List<ProductVM>
        
    }
}