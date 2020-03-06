using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Repositories.Interfaces;
using Forms.Core.Services.Interfaces;
using Forms.Core.SubmissionHandlers.Interfaces;
using Forms.Core.SubmissionHandlers.Models;

namespace Forms.Core.SubmissionHandlers
{
    public class SubmissionHandlerStrategy : ISubmissionHandlerStrategy
    {
        private readonly IEnumerable<ISubmissionHandler> _handlers;
        private readonly IFormRepository _formRepository;
        private readonly IReferenceNumberService _referenceNumberService;

        public SubmissionHandlerStrategy(IEnumerable<ISubmissionHandler> handlers, IFormRepository formRepository, IReferenceNumberService referenceNumberService)
        {
            _handlers = handlers;
            _formRepository = formRepository;
            _referenceNumberService = referenceNumberService;
        }

        public async Task<SubmissionResponse> Handle(FormType formType, string formKey, InFlightPage consentPage)
        {
            var handler = _handlers.First(x => x.FormType == formType);

            var form = await _formRepository.GetForm(formKey);

            var uniqueReferenceId = _referenceNumberService.GenerateReferenceNumber(formKey);
            
            return await handler.Handle(form, uniqueReferenceId, consentPage);
        }
    }
}