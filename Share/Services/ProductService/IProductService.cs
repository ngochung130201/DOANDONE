using Share.Hepers.PagedList;
using Share.Models;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.ProductService
{
    public interface IProductService
    {
        public Task<PagingResponseModel<List<GetAllProductVM>>> GetAllProductAsync(int currentPageNumber, int pageSize,
            string sort, string dir, string where, string search);
        public Task<bool> AddProductAsync(ProductVM Product);
        public Task<Product> GetProductAsync(string slug);
        public Task<bool> UpdateProductAsync(string slug, ProductVM Product);
        public Task<bool> RemoveProductAsync(Product product);
        public Task RemoveProductAllAsync(Guid[] ProductId);
        public Task<Product> UpdateProductViewCount(string slug);


    }
}
