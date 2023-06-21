using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.MenuService
{
    public interface IMenuService
    {
     
        Task<List<Menu>> GetAllAsync();
        Task<Menu> GetByWhereAsync(int id);
        Task<Menu> AddEnity(Menu enity);
        Task<bool> UpdateEnity(int Id, MenuVM enity);
        Task<bool> DeleteEnity(Menu enity);
       
    }
}
