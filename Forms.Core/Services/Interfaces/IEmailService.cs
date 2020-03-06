using System.Threading.Tasks;
using Forms.Core.Models.Email;

namespace Forms.Core.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendAFIPEmail(AfipEmailContent content);
        Task SendAFIPUserEmail(string recipient, UserEmailContent content);
        Task SendAFCSEmail(AfcsEmailContent content);
        Task SendAFCSUserEmail(string recipient, UserEmailContent content);
    }
}