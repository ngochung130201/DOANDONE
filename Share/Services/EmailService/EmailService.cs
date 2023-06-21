using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Share.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Text;
using Share.Models;

namespace Share.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void HandleSendMail(EmailVM req,List<OrderDetail> orderDetails,decimal sumPrice)
        {

            var host = _config["EmailSettings:Host"];
            var port = int.Parse(_config["EmailSettings:Port"]);
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];
            var senderName = _config["EmailSettings:SenderName"];
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var recipientName = req.Name;
            var recipientEmail = req.To;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(recipientName, recipientEmail));
            message.Subject = req.Subject;

            var htmlContent = GenerateHtmlContent(orderDetails,sumPrice);

            message.Body = new TextPart(TextFormat.Html) { Text = htmlContent };

            using (var client = new SmtpClient())
            {
                client.Connect(host, port);
                client.Authenticate(username, password);

                client.Send(message);
                client.Disconnect(true);
            }

        }
        private string GenerateHtmlContent(List<OrderDetail> orderDetails,decimal sumPrice)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("table { border-collapse: collapse; }");
            sb.AppendLine("th, td { border: 1px solid #000; padding: 5px; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Số thứ tự</th>");
            sb.AppendLine("<th>Tên sản phẩm</th>");
            sb.AppendLine("<th>Giá</th>");
            sb.AppendLine("<th>Liên kết</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");
            int counter = 1;
            foreach (var item in orderDetails)
            {
                sb.AppendLine("<tr>");
                 sb.AppendLine($"<td>{counter++}</td>");
                sb.AppendLine($"<td>{item.Product.Name}</td>");
                sb.AppendLine($"<td>{item.Price}</td>");
                sb.AppendLine($"<td><a href='{item.Product.Slug}' target=”_blank””>{item.Product.Slug}</a></td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine($"Tổng tiền={sumPrice}");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

    }

}