using BaseCore.Common.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCore.MailServices
{
    public interface IMailSerivces
    {
        Task SendMailAsync(EmailModel emailModel);
    }
}
