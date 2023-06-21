using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Share.Models;
using Share.ViewModels;

namespace Share.Services.EmailService
{
   public interface IEmailService
    {
        void HandleSendMail(EmailVM req,List<OrderDetail> orderDetails,decimal sumPrice);
    }
}