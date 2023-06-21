using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.PostCategoryService
{
    public interface IPostCategoryService
    {

        Task<List<PostCategory>> GetAllAsync();
        Task<PostCategory> GetByWhereAsync(string slug);
        Task<PostCategory> AddEnity(PostCategoryVM enity);
        Task<bool> UpdateEnity(string slug, PostCategoryVM enity);
        Task<bool> DeleteEnity(PostCategory enity);
        public Task<bool> DeleteEnitys(string[] slugs);

    }
}
