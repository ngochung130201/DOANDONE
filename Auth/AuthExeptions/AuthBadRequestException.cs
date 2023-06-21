using Auth.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthExeptions
{
    public class AuthBadRequestException : BadRequestException
    {
        public AuthBadRequestException(string message) : base(message)
        {
        }
    }
}
