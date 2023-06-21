using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.PostCommentService
{
    public interface IPostCommentService 
    {

        Task<List<PostComment>> GetAllAsync();
        Task<PostComment> GetByWhereAsync(Guid id);
        Task<PostComment> AddEnity(PostCommentVM enity);
        Task<bool> UpdateEnity(Guid id, PostCommentVM enity);
        Task DeleteEnity(PostComment enity);

    }
}
