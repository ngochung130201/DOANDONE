using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.AboutService
{
    public interface IAboutService 
    {
     
        Task<List<About>> GetAllAsync();
        Task<About> GetByWhereAsync(int id);
        Task<About> AddEnity(AboutVM enity);
        Task<bool> UpdateEnity(int Id, AboutVM enity);
        Task<bool> DeleteEnity(About enity);
       
    }
}
