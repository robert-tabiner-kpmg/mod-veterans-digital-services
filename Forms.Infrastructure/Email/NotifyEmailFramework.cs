using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Frameworks;
using Microsoft.Extensions.Options;
using Notify.Client;
using Notify.Interfaces;

namespace Forms.Infrastructure.Email
{
    public class NotifyEmailFramework : IEmailFramework
    {
        private readonly IAsyncNotificationClient _notificationClient;
        public NotifyEmailFramework(IOptions<EmailFrameworkOptions> options)
        {
            _notificationClient = new NotificationClient(options.Value.ApiKey);
        }
        
        public async Task SendEmail(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation)
        {
            await _notificationClient.SendEmailAsync(emailAddress, templateId, personalisation);
        }
    }
}