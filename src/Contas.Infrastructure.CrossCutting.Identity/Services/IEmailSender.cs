using System.Threading.Tasks;

namespace Contas.Infrastructure.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
