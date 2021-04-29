using BaseCore.Common.Models.Emails;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCore.MailServices
{
    public class MailService : IMailSerivces
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSetting)
        {
            _mailSettings = mailSetting.Value;
        }
        public async Task SendMailAsync(EmailModel emailModel)
        {
            //use Mime tool
            var mail = new MimeMessage();
            // get information mail admin
            mail.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //get mail from to 
            mail.To.Add(MailboxAddress.Parse(emailModel.ToEmail));
            mail.Subject = emailModel.Subject;
            var builder = new BodyBuilder();
            // list file send
            if(emailModel.Attachments != null)
            {
                byte[] fileBytes;
                foreach(var file in emailModel.Attachments)
                {
                    if(file.Length > 0)
                    {
                        using(var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            // bind data to body
            builder.HtmlBody = emailModel.Body;
            mail.Body = builder.ToMessageBody();
            // send mail
            using var stmp = new SmtpClient();
            stmp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            stmp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await stmp.SendAsync(mail);
            stmp.Disconnect(true);
        }

        public async Task SenMailTemplateHTML(EmailModel emailModel)
        {
            var mail = new MimeMessage();
            mail.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            mail.To.Add(MailboxAddress.Parse(emailModel.ToEmail));
            mail.Subject = $"Welcome to {emailModel.Subject}";
            var builder = new BodyBuilder();
            builder.HtmlBody = $"<div style='background:#dff0d8; border-left:4px solid #468847; padding:8px; text-align:center'>" +
                                                    "<h3 style='font-size:16px; color:#333; margin:0'>Bạn đã đổi mật khẩu thành công!</h3><br /><h4 style='font-size:15px; font-weight:600;color:#888'>Mật khẩu mới của bạn là:  </h4>" +
                                                "</div>";
            mail.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(mail);
            smtp.Disconnect(true);
        }
    }
}
