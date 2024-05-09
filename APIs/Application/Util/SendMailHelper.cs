using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Util
{
    public class SendMailHelper : ISendMailHelper
    {
        private readonly IConfiguration _configuration;
        public SendMailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendMailAsync(string email, string subject, string message)
        {
            try
            {
                var _email = _configuration["EmailSetting:Email"];
                var _epass = _configuration["EmailSetting:Password"];
                var _dispName = _configuration["EmailSetting:DisplayName"];
                MailMessage myMessage = new MailMessage();
                myMessage.IsBodyHtml = true;
                myMessage.To.Add(email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public async Task<bool> SendMailAsync(List<string> emails, string subject, string message)
        {
            try
            {
                var _email = _configuration["EmailSetting:Email"];
                var _epass = _configuration["EmailSetting:Password"];
                var _dispName = _configuration["EmailSetting:DisplayName"];
                MailMessage myMessage = new MailMessage();
                foreach (var email in emails)
                {
                    myMessage.To.Add(email);
                }
                myMessage.IsBodyHtml = true;
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    public interface ISendMailHelper
    {
        public Task<bool> SendMailAsync(string email, string subject, string message);
        public Task<bool> SendMailAsync(List<string> emails, string subject, string message);
    }
}
