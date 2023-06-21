using Microsoft.AspNetCore.Identity;
using Share.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Models
{
    public class UserDentity : IdentityUser
    {
    
        public Guid MemberId { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { set; get; } // ngày sinh nhật
        public string? Telephone { set; get; } // số điện thoại
        public string? About { set; get; } // giới thiệu cá nhân
      
        public string? Avatar { set; get; } // ảnh đại diện

        public bool RememberMe { set; get; } // có nhớ mật khẩu không
        public DateTime DateCreated { set; get; } = DateTime.Now; // ngày tạo =
        public bool Active { get; set; } = true;


    }
}
