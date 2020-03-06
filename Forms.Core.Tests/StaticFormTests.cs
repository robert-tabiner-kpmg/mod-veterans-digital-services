using System.Collections.Generic;
using System.Linq;
using Forms.Core.Forms;
using Forms.Core.Forms.Afcs;
using Forms.Core.Forms.Afip;
using Forms.Core.Models.Static;
using Forms.Core.Repositories.Interfaces;
using Forms.Core.Services;
using Forms.Core.Services.Interfaces;
using Moq;
using Xunit;

namespace Forms.Core.Tests
{
    public class StaticFormTests
    {
        public static IEnumerable<object[]> Forms = new[]
        {
            new object[] {Afip.Form},
            new object[] {Afcs.Form},
        };

        [Theory]
        [MemberData(nameof(Forms))]
        public void AllTasksHaveUniqueIds(Form form)
        {
            foreach (var section in form.Tasks)
            {
                Assert.NotNull(section.Id);
                Assert.Single(form.Tasks.Where(x => x.Id == section.Id));
            }
        }


        [Theory]
        [MemberData(nameof(Forms))]
        public void AllQuestionsAndSubTasksHaveIds__AndTheIdsAreUniqueWithinItsContainer(Form form)
        {
            foreach (var task in form.Tasks)
            {
                CheckTaskItems(task.TaskItems);
            }
            
            void CheckTaskItems(List<ITaskItem> taskItems)
            {
                foreach (var item in taskItems)
                {
                    Assert.NotNull(item.Id);
                    Assert.Single(taskItems.Where(x => x.Id == item.Id));

                    if (item is SubTask st)
                    {
                        CheckTaskItems(st.TaskItems);
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(Forms))]
        public void AllNextPageIdsExist(Form form)
        {
            foreach (var task in form.Tasks)
            {
                CheckTaskItems(task.TaskItems);
            }
            
            void CheckTaskItems(List<ITaskItem> taskItems)
            {
                foreach (var item in taskItems)
                {
                    if (item.NextPageId != null)
                    {
                        Assert.NotNull(taskItems.FirstOrDefault(x => x.Id == item.NextPageId));
                    }

                    if (item is SubTask st)
                    {
                        CheckTaskItems(st.TaskItems);
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(Forms))]
        public void AllTasksWithMoreThanOneTaskHaveATaskList(Form form)
        {
            if (form.Tasks.Count > 1)
            {
                Assert.NotNull(form.TaskListPage);
            };
        }

        [Theory]
        [MemberData(nameof(Forms))]
        public void WhenFormHasATaskList__AllTasksHaveATaskListGroup(Form form)
        {
            if (form.TaskListPage != null)
            {
                foreach (var task in form.Tasks)
                {
                    Assert.True(task.GroupNameIndex < form.TaskGroups.Count);
                }
            }
        }

        [Theory]
        [MemberData(nameof(Forms))]
        public async System.Threading.Tasks.Task AGraphCanBeConstructedUsingTheForm(Form form) // checks no errors are thrown in the graph construction
        {
            // Arrange
            var repositoryMock = new Mock<IFormRepository>();
            var formProvider = new Mock<IStaticFormProvider>();
            formProvider.Setup(x => x.GetForm(It.IsAny<FormType>())).Returns(form);
            var formService = new FormService(repositoryMock.Object, formProvider.Object);
            
            // Act
            var result = await formService.InitialiseForm("test", default);
            
            // Assert
            Assert.NotNull(result);
        }
    }
}
