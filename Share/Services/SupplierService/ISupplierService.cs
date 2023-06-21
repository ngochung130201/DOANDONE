using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.SupplierService
{
    public interface ISupplierService 
    {

        Task<List<Supplier>> GetAllAsync();
        Task<Supplier> GetByWhereAsync(Guid id);
        Task<Supplier> AddEnity(SupplierVM enity);
        Task<bool> UpdateEnity(Guid id, SupplierVM enity);
        Task<bool> DeleteEnity(Supplier enity);

    }
}
