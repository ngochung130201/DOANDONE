﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class ResetPasswordModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string Token { get; set; }
    }
}
