using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Interfaces.Utility
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendConfirmationEmailAsync(SignUpUserVM request, string code);
        bool EmailIsValid(string email);
    }
}
