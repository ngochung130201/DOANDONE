using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByWhereAsync(int id);
        Task<T> AddEnity(T enity);
        Task<bool> UpdateEnity(int Id, T enity);
        Task<bool> DeleteEnity(T enity);
    }
}
