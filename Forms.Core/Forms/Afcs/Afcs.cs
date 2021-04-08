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
                "Who is making this application?",
                "About you",
                "Your claim",
                "Other details",
                "Your payment details",
                
                "Declaration and application submission"
            },
            Tasks = new List<Task>
            {
                ThingsToKnow.Task,
                NominateRepresentativeApplicant.Task,
                NominateRepresentative.Task,
                PersonalDetails.Task,
                ServiceDetails.Task,
                ClaimDetails.Task,
                OtherMedicalTreatment.Task,
                OtherCompensation.Task,
                AboutBenefits.Task,
                PaymentDetails.Task,
                
                Declarations.Task
            },
            StartPage = new ContentPage
            {
                Header = "Claim an Armed Forces Compensation Scheme or War Pension payment",
                BodyText = new StringBuilder()
                     .Append("<p><strong>Overview</strong></p>")
                    .Append("<p><a class='govuk-link' href='https://www.gov.uk/guidance/armed-forces-compensation-scheme-afcs'>Armed Forces Compensation Scheme (AFCS)</a> is a compensation scheme for injury or illness " +
                    "which is caused by service in Her Majesty's Armed Forces on or after 6 April 2005.</p>")
                    .Append("<p><a class='govuk-link' href='https://www.gov.uk/guidance/war-pension-scheme-wps'>War Pension Scheme (WPS)</a> " +
                    "is a compensation scheme for veterans for any injury or illness which has been caused by or" +
                    " made worse by service in Her Majesty's Armed Forces up to 5 April 2005.</p>")
                .Append("<p>There is a single combined claim process for both schemes that you can access from this page.</p>")
                .Append("<p><strong>Note for Special Forces Personnel:</strong>If you have served (whether directly or in a support role) " +
                    "with United Kingdom Special Forces (UKSF) must seek advice from the MOD A Block Disclosure Cell before completing the claim form. " +
                    "If you have served at any time after 1996, you will be subject to the UKSF Confidentiality Contract and must apply for " +
                    "Express Prior Authority in Writing (EPAW) through the Disclosure Cell before submitting a claim where you may be asked " +
                    "to disclose details of your service with UKSF or any units directly supporting them.The Disclosure Cell can be contacted " +
                    "by emailing <a href=''> MAB-J1-Disclosures-ISA-Mailbox@mod.gov.uk.</a></p>")
                .Append("<p><strong>Make a claim online</strong></p>")
                .Append("<p>You can use the online claim service to make a claim for an Armed Forces Compensation Scheme or War Pension payment.</p>")
                .Append("<p><strong>Make a claim by post</strong></p>")
                .Append("<p>To apply by post, you can either</p>")
                .Append("<ul><li>Download a claim form from the <a class='govuk-link' href='https://www.gov.uk/guidance/war-pension-scheme-wps'>War Pension Scheme</a> or" +
                    "<a class='govuk-link' href='https://www.gov.uk/guidance/armed-forces-compensation-scheme-afcs'> Armed Forces Compensation Scheme </a> pages, " +
                    "complete it and post it back to us, or </li>")
                .Append("<li>Request a paper claim form from the <a class='govuk-link' href='https://www.gov.uk/guidance/veterans-uk-contact-us'>Veterans UK Helpline</a></li></ul>")
                .Append("<p><strong>Help to claim</strong></p>")
                 .Append("<p>If you need help making a claim, please contact the <a class='govuk-link' href='https://www.gov.uk/guidance/veterans-uk-contact-us'>Veterans UK Helpline </a> for assistance. Where needed, " +
                    "our <a class='govuk-link' href='https://www.gov.uk/guidance/veterans-welfare-service'>Veterans Welfare Service</a> can also arrange an appointment to provide one to one help with making a " +
                    "claim at a time that is convenient to you.</p>")
                 .Append("<p>You do not need a paid representative such as a solicitor or claims management company " +
                    "to apply for compensation. Free independent advice is available from the <a class='govuk-link' href='https://www.gov.uk/guidance/veterans-welfare-service'>" +
                    "Veterans Welfare Service</a> or other <a class='govuk-link' href='https://www.gov.uk/guidance/charity-support-for-veterans'>charitable organisations</a></p>")
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