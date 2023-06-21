

using Share.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ListException.Brand
{
    public class BrandOKException : OKRequestException
    {
        public BrandOKException(string message) : base(message)
        {
        }
    }
}
