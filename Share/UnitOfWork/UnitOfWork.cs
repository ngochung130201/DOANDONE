using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Share.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {

       private T dbContext;
        private object[] paramItems;
        public UnitOfWork(T context)
        {
            dbContext = context;
           


        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
      

       
    }
}