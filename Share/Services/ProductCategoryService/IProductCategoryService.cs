using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.ProductCategoryService
{
    public interface IProductCategoryService
    {

        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory> GetByWhereAsync(string slug);
        Task<ProductCategory> AddEnity(ProductCategoryVM enity);
        Task<bool> UpdateEnity(string slug, ProductCategoryVM enity);
        Task<bool>  DeleteEnity(ProductCategory enity);

    }
}
