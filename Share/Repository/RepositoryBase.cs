using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Share.Data;
using Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private DoAnBanHangContext _dbContext;
     
        internal DbSet<T> DbSet { get; set; }
        public RepositoryBase(DoAnBanHangContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<T>();
      

        }

        public async Task<T> AddEnity(T enity)
        {
   
            await DbSet.AddAsync(enity);

            return enity;
        }

        public async Task<bool> DeleteEnity(T enity)
        {
            DbSet.Remove(enity);


            return true;
        }

        public async Task<List<T>> GetAllAsync()
        {

            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByWhereAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<bool> UpdateEnity(int Id, T enity)
        {

            DbSet.Attach(enity);
            _dbContext.Entry(enity).State = EntityState.Modified;

            return true;
        }
        // searh pagging sort

    }
}
