using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.FeedbackService
{
    public interface IFeedbackService
    {

        Task<List<Feedback>> GetAllAsync();
        Task<Feedback> GetByWhereAsync(int id);
        Task<Feedback> AddEnity(FeedbackVM enity);
        Task<bool> UpdateEnity(int Id, FeedbackVM enity);
        Task<bool> DeleteEnity(Feedback enity);
        public Task<bool> DeleteEnitys(int[] Ids);

    }
}
