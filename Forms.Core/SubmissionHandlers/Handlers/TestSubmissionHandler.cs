using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.Display;
using Forms.Core.Models.Email;
using Forms.Core.Models.InFlight;
using Forms.Core.Services.Interfaces;
using Forms.Core.SubmissionHandlers.Interfaces;
using Forms.Core.SubmissionHandlers.Models;
using Graph.Models;
using Microsoft.Extensions.Primitives;

namespace Forms.Core.SubmissionHandlers.Handlers
{
    public class TestSubmissionHandler : ISubmissionHandler
    {
        public FormType FormType => FormType.Test;

        public TestSubmissionHandler()
        {
        }

        public Task<SubmissionResponse> Handle(Graph<Key, FormNode> graph, string referenceNumber,
            InFlightPage consentPage)
        {
            return Task.FromResult(new SubmissionResponse
            {
                Reference = referenceNumber,
            });
        }
    }
}