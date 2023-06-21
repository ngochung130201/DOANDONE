using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.UnitOfWork
{
    public interface IUnitOfWork<T> where T : DbContext
    {
        int SaveChanges();
    }
}
