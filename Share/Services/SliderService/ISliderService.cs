using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.SliderService
{
    public interface ISliderService
    {
     
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByWhereAsync(int id);
        Task<Slider> AddEnity(SliderVM enity);
        Task<bool> UpdateEnity(int Id, SliderVM enity);
        Task<bool> DeleteEnity(Slider enity);
       
    }
}
