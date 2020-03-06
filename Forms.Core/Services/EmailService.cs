using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Frameworks;
using Forms.Core.Models.Email;
using Forms.Core.Options;
using Forms.Core.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Forms.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailFramework _emailFramework;
        private readonly EmailTemplateOptions _emailTemplateOptions;
        private readonly bool _isDevelopment;
        public EmailService(IEmailFramework emailFramework, IOptions<EmailTemplateOptions> emailTemplateOptions, IHostEnvironment hostEnvironment)
        {
            _emailFramework = emailFramework;
            _emailTemplateOptions = emailTemplateOptions.Value ?? throw new ArgumentException("Email template options not provided");
            _isDevelopment = hostEnvironment.IsDevelopment();
        }
        
        public async Task SendAFIPEmail(AfipEmailContent content)
        {
            var contentDictionary = MapEmailProperties(content);
            await _emailFramework.SendEmail(_emailTemplateOptions.AFIPEmailRecipient, _emailTemplateOptions.AFIPTemplateId, contentDictionary);
        }

        public async Task SendAFIPUserEmail(string recipient, UserEmailContent content)
        {
            var contentDictionary = MapEmailProperties(content);
            await _emailFramework.SendEmail(_isDevelopment ? _emailTemplateOptions.DevelopmentUserEmailRecipient : recipient, _emailTemplateOptions.AFIPUserTemplateId, contentDictionary);
        }

        public async Task SendAFCSEmail(AfcsEmailContent content)
        {
            var contentDictionary = MapEmailProperties(content);
            await _emailFramework.SendEmail(_emailTemplateOptions.AFCSEmailRecipient, _emailTemplateOptions.AFCSTemplateId, contentDictionary);
        }

        public async Task SendAFCSUserEmail(string recipient, UserEmailContent content)
        {
            var contentDictionary = MapEmailProperties(content);
            await _emailFramework.SendEmail(_isDevelopment ? _emailTemplateOptions.DevelopmentUserEmailRecipient : recipient, _emailTemplateOptions.AFCSUserTemplateId, contentDictionary);
        }

        Dictionary<string, dynamic> MapEmailProperties<T>(T emailContent)
        {
            var content = new Dictionary<string, dynamic>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                foreach(var attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is EmailContentAttribute contentAttribute)
                    {
                        var value = propertyInfo.GetValue(emailContent);
                        if (value is null)
                        {
                            throw new ArgumentException($"Missing email content: {propertyInfo.Name}");
                        }
                        content.Add(contentAttribute.PropertyName, propertyInfo.GetValue(emailContent));
                    }
                }
            }

            return content;
        }
    }
}