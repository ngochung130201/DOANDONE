
using Auth.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthExeptions
{
    public class AuthOKException : OKRequestException
    {
        public AuthOKException(string message) : base(message)
        {
        }
    }
}
