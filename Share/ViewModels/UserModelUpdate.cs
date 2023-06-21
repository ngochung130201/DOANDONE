using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.ViewModels
{
    public class UserModelUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { set; get; } // ngày sinh nhật
        public string PhoneNumber { set; get; } // số điện thoại
    }
}