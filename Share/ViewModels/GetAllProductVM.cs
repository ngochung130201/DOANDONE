using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ViewModels
{
    public class GetAllProductVM
    {

        public Guid ProductID { get; set;}
        public string ?Name { get; set;}
        public string Slug { get; set;}
        public string ?Image { get; set;}
        public bool ?IsFreeship { get; set;}
        public decimal? Price { get; set;}
        public decimal? PromotionPrice { get; set;}
        public int? Quantity { get; set;}
        public DateTime? Hot { get; set;}
        public int? ViewCount { get; set;}
        public DateTime? CreateDate { get; set;}
        public DateTime? UpdateDate { get;}
        public string? BrandName { get;}
        public string? SupplierName { get;}
        public string? ProductCateName { get;}
        public int? BrandID { get;}
        public Guid? SupplierID { get; set; }
        public Guid? CateID { get; set;}
   
    }
}
