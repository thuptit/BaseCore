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

            var mail = new MimeMessage();
            mail.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            mail.To.Add(MailboxAddress.Parse(emailModel.ToEmail));
            emailModel.Subject = emailModel.Subject;
            var builder = new BodyBuilder();
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
            builder.HtmlBody = emailModel.Body;
            mail.Body = builder.ToMessageBody();
            using var stmp = new SmtpClient();
            stmp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            stmp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await stmp.SendAsync(mail);
            stmp.Disconnect(true);
        }
    }
}
