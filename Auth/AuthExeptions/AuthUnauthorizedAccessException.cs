
using Auth.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.AuthExeptions
{
    public class AuthUnauthorizedAccessException : UnauthorizedException
    {
        public AuthUnauthorizedAccessException(string message) : base(message)
        {
        }
    }
}
