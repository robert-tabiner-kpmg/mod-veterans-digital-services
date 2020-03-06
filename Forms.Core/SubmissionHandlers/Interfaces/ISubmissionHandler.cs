using System.Threading.Tasks;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.SubmissionHandlers.Models;
using Graph.Models;

namespace Forms.Core.SubmissionHandlers.Interfaces
{
    public interface ISubmissionHandler
    {
        FormType FormType { get; }
        Task<SubmissionResponse> Handle(Graph<Key, FormNode> graph, string referenceNumber, InFlightPage consentPage);
    }
}