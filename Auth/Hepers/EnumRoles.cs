using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Hepers
{
    [Flags]
    public  enum EnumRoles
    {
        SuperAdmin = 1 ,
        Manager,
        User
    }
}
