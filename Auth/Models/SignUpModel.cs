using Share.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }

        public bool Active { get; set; } = true;

        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        //public UserDentity Member { get; set; }
    }
}
