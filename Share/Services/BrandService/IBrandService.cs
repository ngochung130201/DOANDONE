using Microsoft.AspNetCore.Http;
using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.BrandService
{
    public interface IBrandService
    {

        Task<List<Brand>> GetAllAsync();
        Task<Brand> GetByWhereAsync(int id);
        Task<Brand> AddEnity(BrandVM enity);
        Task<bool> UpdateEnity(int Id, BrandVM enity);
        Task<bool> DeleteEnity(int Id);
        Task<bool> DeleteEnitys(int[] Ids);

    }
}
