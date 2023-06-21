using Share.Models;
using Share.Repository;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Services.ContactService
{
    public interface IContactService
    {

        Task<List<Contact>> GetAllAsync();
        Task<Contact> GetByWhereAsync(int id);
        Task<Contact> AddEnity(ContactVM enity);
        Task<bool> UpdateEnity(int Id, ContactVM enity);
        Task<bool> DeleteEnity(Contact enity);
        public Task<bool> DeleteEnitys(int[] Ids);


    }
}
