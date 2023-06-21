using Share.ListException.Brand;
using Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Hepers
{
    public class FuncHelper<T> 
    {
        public static async Task<T> CheckEnity(T enity)
        {
            if (enity == null)
            {
                throw new BrandNotFoundException("Thương hiệu không tồn tạị");
            }

            return enity;
        }
    }
}
