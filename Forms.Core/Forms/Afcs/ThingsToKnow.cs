using System.Collections.Generic;
using System.Text;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Questions;
using Forms.Core.Models.Static;

namespace Forms.Core.Forms.Afcs
{
    public static class ThingsToKnow
    {
        public static Task Task => new Task
        {
            Id = "things-to-know-task",
            Name = "Things to know before you start",
            GroupNameIndex = 0,
            SummaryPage = null,
            PostTaskPage = null,
            PreTaskPage = null,
            TaskItems = new List<ITaskItem>
            {
                new TaskQuestionPage
                {
                    Id = "check-before-start",
                    NextPageId = null,
                    Header = "Things you need to know before you start",
                    IncludeQuestionNumber = false,
                    Questions = new List<BaseQuestion>
                    {
                        new HiddenQuestion
                        {
                            Id = "question1",
                            BodyText = new StringBuilder()
                                .Append(
                                    "<p>Use this service to:</p>")
                            .Append("<ul>")
                                .Append(
                                    "<li>make a claim for an Armed Forces Compensation Scheme or War Pension Scheme payment.</li>")
                                .Append(
                                    "<li>This single claim service is for both schemes.</li>")
                             .Append(
                                    "</ul>")
                                .Append(
                                    "<h1 class=\"govuk-heading-m\">Making a claim</h1>")
                                .Append("<ul>")
                                .Append(
                                    "<li>You will be asked a series of questions about yourself, your service and the medical conditions you are claiming for.</li>")
                                .Append(
                                    "<li>You can make multiple claims in one application by ‘adding a further claim’ within the ‘claim’ section.</li>")
                                .Append(
                                    "<li>You can return to a partially completed application up to 3 months from when you started, providing you have fully completed the “Personal Details” section before you leave.</li>")
                                
                                .Append(
                                    "</ul>")
                                .Append("<h1 class=\"govuk-heading-m\">What you need to apply</h1>")
                             .Append(
                                    "<p>You’ll need:</p>")
                                .Append("<ul>")
                                .Append(
                                    "<li>your bank, building society or credit union account details.</li>")
                                .Append(
                                    "<li>an email address.</li>")
                                .Append(
                                    "<li>details about your service in the armed forces.</li>")
                                .Append(
                                    "<li>information about the injury/illness you are claiming for.</li>")
                                .Append(
                                    "<li>letters/reports already in your possession from medical professionals who have treated you.</li>")
                                .Append(
                                    "<li>details of any other compensation you may have received. For example; <p>Criminal Injuries Compensation.</p><p>Civil Negligence claims.</p><p>Industrial Injuries Disablement Benefit.</p></li>")
                                .Append(
                                    "</ul>")
                                .Append(
                                    "<p><b>We don’t need you to get any new information you do not already have. We can’t refund any costs involved if you do this.</b></p>")

                                .Append("<h1 class=\"govuk-heading-m\">Dates, addresses and contact details:</h1>")
                            .Append(
                                    "<p>Several questions ask about dates and contact details relating to your service and places you have received medical treatment.  Please enter details to the best of your recollection, even if they are not complete.  For example, if you cannot recall exact dates, provide a best estimate even if this is just a year. You do not need to research details you do not already have and do not delay submitting your application.</p>")

                             .Append("<h1 class=\"govuk-heading-m\">After you apply</h1>")
                            .Append("<ul>")
                                .Append(
                                    "<li>We will register your claim and send you an acknowledgement.</li>")
                                .Append(
                                    "<li>We will gather information and medical evidence to support your claim.</li>")
                                .Append(
                                    "<li>After careful consideration of all the evidence, a decision will be made.</li>")
                             .Append(
                                    "<li>We will contact you with the outcome of your claim.</li>")
                                .Append(
                                    "</ul>")
                             .Append(
                                    "<p>Depending on the nature of the injury/illness and the complexity of your claim, consideration of the evidence can take several months. Please be assured we will contact you as soon as a decision is made.</p>")

                                
                                .ToString()
                        }
                    }
                }
            }
        };
    }
}