using SchoolDemo.Models;
using System.Threading.Tasks;

namespace SchoolDemo.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendForgetPassword(UserEmailOptions userEmailOptions);
    }
}