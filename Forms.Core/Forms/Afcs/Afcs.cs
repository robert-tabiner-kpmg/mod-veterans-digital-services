using System.Collections.Generic;
using System.Text;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Static;

namespace Forms.Core.Forms.Afcs
{
    public static class Afcs
    {
        public static Form Form => new Form
        {
            Name = "Apply for the Armed Forces Compensation Scheme",
            TaskListPage = new TaskListPage(),
            TaskGroups = new List<string>
            {
                "Check before you start",
                "About you",
                "Your claim",
                "Other details",
                "Your payment details",
                "Nominate a representative",
                "Declaration and application submission"
            },
            Tasks = new List<Task>
            {
                ThingsToKnow.Task,
                PersonalDetails.Task,
                ServiceDetails.Task,
                ClaimDetails.Task,
                OtherMedicalTreatment.Task,
                OtherCompensation.Task,
                AboutBenefits.Task,
                PaymentDetails.Task,
                NominateRepresentative.Task,
                Declarations.Task
            },
            StartPage = new ContentPage
            {
                Header = "Armed Forces Compensation Scheme and War Pensions Scheme claim form",
                BodyText = new StringBuilder()
                    .Append("<p>Use this form when claiming under the Armed Forces Compensation Scheme and/or the War Pensions Scheme</p>")
                .Append("<p><strong>The Armed Forces Compensation Scheme applies to:</strong></p>")
                .Append("<ul><li>injury or illness is due to service on or after 6th April 2005</li></ul>")
                .Append("<p><strong>The War Pension Scheme applies to:</strong></p>")
                .Append("<ul><li>injury or illness is due to service on or after 6th April 2005</li></ul>")
                .ToString()
            },
            TermsAndConditions = new ContentPage
            {
                Header = "Claim Form",
                BodyText = new StringBuilder()
                    .Append("<p><strong>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. " +
                            "Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. " +
                            "Praesent mauris. Fusce nec tellus sed augue semper porta. Mauris massa. Vestibulum lacinia arcu eget nulla. " +
                            "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur sodales " +
                            "ligula in libero. Sed dignissim lacinia nunc.</strong></p>")
                    .Append("<p>Curabitur tortor. Pellentesque nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed " +
                            "convallis tristique sem. Proin ut ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit " +
                            "quis, luctus non, massa. Fusce ac turpis quis ligula lacinia aliquet. Mauris ipsum. Nulla metus metus, ullamcorper " +
                            "vel, tincidunt sed, euismod in, nibh. Quisque volutpat condimentum velit.</p>")
                    .Append("<p>Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nam nec ante. " +
                            "Sed lacinia, urna non tincidunt mattis, tortor neque adipiscing diam, a cursus ipsum ante quis turpis. " +
                            "Nulla facilisi. Ut fringilla. Suspendisse potenti. Nunc feugiat mi a tellus consequat imperdiet. Vestibulum sapien. " +
                            "Proin quam. Etiam ultrices. Suspendisse in justo eu magna luctus suscipit. Sed lectus. Integer euismod lacus luctus " +
                            "magna. Quisque cursus, metus vitae pharetra auctor, sem massa mattis sem, at interdum magna augue eget diam.</p>")
                    .ToString()
            }
        };
    }
}