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
                                    "<p>Things we need to see about your claimed condition. If they apply to your claim then please gather them together before you start.</p>")
                                .Append(
                                    "<p>If you do not have copies of the documents we ask for, do not delay in returning your claim form. You may send us additional information or documents later</p>")
                                .Append(
                                    "<h1 class=\"govuk-heading-m\">Letters and reports from people who have treated you</h1>")
                                .Append("<ul>")
                                .Append(
                                    "<li>GP's</li>")
                                .Append(
                                    "<li>Hospital doctors</li>")
                                .Append(
                                    "<li>Specialist nurses</li>")
                                .Append(
                                    "<li>Psychiatrists or Psychologists at consultant grade</li>")
                                .Append(
                                    "<li>Occupational therapists</li>")
                                .Append(
                                    "<li>Physiotherapist</li>")
                                .Append(
                                    "</ul>")
                                .Append("<h1 class=\"govuk-heading-m\">Service documents such as</h1>")
                                .Append("<ul>")
                                .Append(
                                    "<li>Part 1 orders</li>")
                                .Append(
                                    "<li>Admin instructions</li>")
                                .Append(
                                    "<li>Authorisation papers</li>")
                                .Append(
                                    "<li>Accident report forms</li>")
                                .Append(
                                    "<li>Hurt certificates</li>")
                                .Append(
                                    "<li>Service medical records</li>")
                                .Append(
                                    "</ul>")
                                .Append("<h1 class=\"govuk-heading-m\">Medical test results including</h1>")
                                .Append("<ul>")
                                .Append(
                                    "<li>Scans</li>")
                                .Append(
                                    "<li>Audiology</li>")
                                .Append(
                                    "<li>Reports of x-rays, but not the x-rays themselves</li>")
                                .Append(
                                    "</ul>")
                                .Append("<h1 class=\"govuk-heading-m\">Letters about payment of</h1>")
                                .Append("<ul>")
                                .Append(
                                    "<li>Criminal injuries compensation</li>")
                                .Append(
                                    "<li>Civil negligence compensation</li>")
                                .Append(
                                    "<li>Industrial injuries disablement benefit</li>")
                                .Append(
                                    "</ul>")
                                .Append("<h1 class=\"govuk-heading-m\">Things we don't need to see</h1>")
                                .Append("<ul>")
                                .Append(
                                    "<li>Appointment letters</li>")
                                .Append(
                                    "<li>General information about your medical conditions that are not about you personally</li>")
                                .Append(
                                    "<li>Fact sheets about your medication</li>")
                                .Append(
                                    "<li>Internet printouts</li>")
                                .Append(
                                    "<li>Letters about other benefits</li>")
                                .Append(
                                    "</ul>")
                                .ToString()
                        }
                    }
                }
            }
        };
    }
}