using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.ViewModels
{
    public class MonthlySalesResult
    {
         public int Year { get; set; }
        public int Month { get; set; }
        public int TotalSalesFail { get; set; }
        public int TotalSalesSusses { get; set; }
    }
}