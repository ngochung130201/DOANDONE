using Share.Models;
using Share.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.OrderDetailService
{
    public interface IOrderDetailService
    {
     
        Task<List<OrderDetail>> GetAllAsync();
        Task<OrderDetail> GetByWhereAsync(int id);
        Task<OrderDetail> AddEnity(OrderDetail enity);
        Task<bool> UpdateEnity(int Id, OrderDetail enity);
        Task<bool> DeleteEnity(OrderDetail enity);
       
    }
}
