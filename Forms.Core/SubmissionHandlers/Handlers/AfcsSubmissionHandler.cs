using System.Linq;
using System.Threading.Tasks;
using Forms.Core.Extensions;
using Forms.Core.Forms;
using Forms.Core.Models.Email;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.Services.Interfaces;
using Forms.Core.SubmissionHandlers.Interfaces;
using Forms.Core.SubmissionHandlers.Models;
using Graph.Models;

namespace Forms.Core.SubmissionHandlers.Handlers
{
    public class AfcsSubmissionHandler : ISubmissionHandler
    {
        public FormType FormType => FormType.Afcs;
        
        private readonly IEmailService _emailService;
        private readonly ISummaryService _summaryService;

        public AfcsSubmissionHandler(IEmailService emailService, ISummaryService summaryService)
        {
            _emailService = emailService;
            _summaryService = summaryService;
        }
        
        public async Task<SubmissionResponse> Handle(Graph<Key, FormNode> graph, string referenceNumber,
            InFlightPage consentPage)
        {
            var taskGroupAnswers = _summaryService.GetFormSummary(graph, FormType);

            var recipientEmail = graph.Nodes
                .FindByKey(Key.ForTaskItemPage("personal-details-task", "contact-email", new[] {0}))
                .AssertType<TaskQuestionPageFormNode>().Questions.First(x => x.Id == "question1");

            await _emailService.SendAFCSEmail(new AfcsEmailContent
            {
                ReferenceNumber = referenceNumber,
                PersonalDetails = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["personal-details-task"]),
                ServiceDetails = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["service-details-task"]),
                ClaimDetails = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["claims-details-task"]),
                OtherCompensation = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["other-compensation-task"]),
                OtherBenefits = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["about-benefits-task"]),
                PaymentDetails = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["payment-details-task"]),
                NominateRepresentative = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["nominate-representative-task"]),
                WelfareAgentDetails = TaskGroupingExtensions.BuildFormString(taskGroupAnswers["declarations-task"]),
                ConsentDeclaration = consentPage.Questions.First(x => x.Id == "question1").Answer.AnswerAsString
            });

            await _emailService.SendAFCSUserEmail(recipientEmail.Answer.AnswerAsString, new UserEmailContent
            {
                ReferenceNumber = referenceNumber
            });
            
            return new SubmissionResponse
            {
                Reference = referenceNumber
            };
        }
    }
}