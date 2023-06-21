using Auth.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthExeptions
{
    public sealed class AuthNotFoundException : NotFoundException
    {
        public AuthNotFoundException(string message) : base(message)
        {
        }
    }
}
