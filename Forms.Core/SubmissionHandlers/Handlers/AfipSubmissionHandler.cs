using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.Email;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Services.Interfaces;
using Forms.Core.SubmissionHandlers.Interfaces;
using Forms.Core.SubmissionHandlers.Models;
using Graph.Models;

namespace Forms.Core.SubmissionHandlers.Handlers
{
    public class AfipSubmissionHandler : ISubmissionHandler
    {
        public FormType FormType => FormType.Afip;

        private readonly IEmailService _emailService;
        private readonly ISummaryService _summaryService;

        public AfipSubmissionHandler(IEmailService emailService, ISummaryService summaryService)
        {
            _emailService = emailService;
            _summaryService = summaryService;
        }

        public async Task<SubmissionResponse> Handle(Graph<Key, FormNode> graph, string referenceNumber,
            InFlightPage consentPage)
        {
            var taskGroup = _summaryService.GetFormSummary(graph, FormType);

            var userEmailAddress = graph.Nodes
                .First(x => x.IsTaskQuestionPageNode(out var taskQuestionPageFormNode) &&
                            taskQuestionPageFormNode.PageId == "contact-email").AssertType<TaskQuestionPageFormNode>()
                .Questions.First(x => x.Id == "question1").Answer.AnswerAsString;
            
            await _emailService.SendAFIPEmail(new AfipEmailContent
            {
                ReferenceNumber = referenceNumber,
                FormContent = TaskGroupingExtensions.BuildFormString(taskGroup["afip-form"]),
                ConsentCorrespondEmail = consentPage.Questions.First(x => x.Id == "question1").Answer.AnswerAsString,
                ConsentDwpBenefits = consentPage.Questions.First(x => x.Id == "question2").Answer.AnswerAsString
            });

            await _emailService.SendAFIPUserEmail(userEmailAddress, new UserEmailContent
            {
                ReferenceNumber = referenceNumber
            });

            return new SubmissionResponse
            {
                Reference = referenceNumber,
            };
        }
    }
}