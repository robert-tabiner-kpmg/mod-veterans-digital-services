using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Frameworks
{
    public interface IEmailFramework
    {
        Task SendEmail(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation);
    }
}