

using Share.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.ListException.Brand
{
    public class BrandUnauthorizedAccessException : UnauthorizedException
    {
        public BrandUnauthorizedAccessException(string message) : base(message)
        {
        }
    }
}
