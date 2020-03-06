using System.Threading.Tasks;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.SubmissionHandlers.Models;

namespace Forms.Core.SubmissionHandlers.Interfaces
{
    public interface ISubmissionHandlerStrategy
    {
        Task<SubmissionResponse> Handle(FormType formType, string formKey, InFlightPage consentPage);
    }
}