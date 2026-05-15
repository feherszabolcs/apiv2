using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlBody);
        Task SendRegisterTemplateAsync(string toEmail, string fullName, string guardNumber, string associationName, string confirmUrl);
    }
}