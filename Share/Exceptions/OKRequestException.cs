using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Exceptions
{
    public class OKRequestException : Exception
    {
        public OKRequestException(string message) : base(message)
        { }

    }
}
