using Share.Hepers.PagedList;
using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.PostService
{
    public interface IPostService
    {

        public Task<PagingResponseModel<List<GetAllPosts>>> GetAllPostAsync(int currentPageNumber, int pageSize,
               string sort, string dir, string where, string search);
        public Task<bool> AddPostAsync(PostVM Post);
        public Task<Post> GetPostAsync(string slug);
        public Task<bool> UpdatePostAsync(string slug, PostVM Post);
        public Task<bool> RemovePostAsync(Post Post);
        public Task RemovePostAllAsync(List<PostVM> Post);


    }
}
