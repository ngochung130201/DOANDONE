using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ViewModels
{
    public class MonthlySaleViewModel // tong tien trong 1 thang 
    {
        public int Year { get; set; } // nam
        public int Month { get; set; } // thang
        public decimal TotalAmount { get; set; } // tong cong
        public int OrderCount { get; set; } // don hang
    }
}
