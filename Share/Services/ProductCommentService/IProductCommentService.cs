using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.ProductCommentService
{
    public interface IProductCommentService
    {

        Task<List<ProductComment>> GetAllAsync();
        Task<List<ProductComment>> GetAllAsyncByProductId(string slug);
        Task<ProductComment> GetByWhereAsync(Guid id);
        Task<ProductComment> AddEnity(ProductCommentVM enity);
        Task<bool> UpdateEnity(Guid id, ProductCommentVM enity);
        Task DeleteEnity(ProductComment enity);
        public Task DeleteEnitys(Guid[] ids);

    }
}
