using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.CustomerService
{
    public interface ICustomerService 
    {

        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByWhereAsync(string email);
        Task<Customer> AddEnity(CustomerVM enity);
        Task<bool> UpdateEnity(Guid id, CustomerVM enity);
        Task<bool> DeleteEnity(Customer enity);

    }
}
