using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forms.Core.Exceptions;
using Forms.Core.Forms;
using Forms.Core.Forms.Afcs;
using Forms.Core.Forms.Afip;
using Forms.Core.Forms.Test;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision.Ghost;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Static;
using Forms.Core.Options;
using Forms.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using Task = Forms.Core.Models.Static.Task;

namespace Forms.Core.Services
{
    public class StaticFormProvider : IStaticFormProvider
    {
        private readonly IOptions<FormOptions> _formOptions;

        private readonly Dictionary<FormType, Dictionary<StaticKey, Page>> _formPages;
        private readonly Dictionary<FormType, Dictionary<StaticKey, Task>> _tasks;
        private readonly Dictionary<FormType, Dictionary<StaticKey, SubTask>> _subTasks;
        private readonly Dictionary<FormType, List<string>> _taskGroups;
        private readonly Dictionary<FormType, string> _formNames;

        public StaticFormProvider(IOptions<FormOptions> formOptions)
        {
            _formOptions = formOptions;
            
            if (_formPages is null)
                _formPages = new Dictionary<FormType, Dictionary<StaticKey, Page>>();
            
            if (_tasks is null)
                _tasks = new Dictionary<FormType, Dictionary<StaticKey, Task>>();
            
            if (_taskGroups is null)
                _taskGroups = new Dictionary<FormType, List<string>>();

            if (_formNames is null)
                _formNames = new Dictionary<FormType, string>();
            
            if (_subTasks is null)
                _subTasks = new Dictionary<FormType, Dictionary<StaticKey, SubTask>>();
        }
        
        public Form GetForm(FormType formType)
        {
            return formType switch
            {
                FormType.Afcs => Afcs.Form,
                FormType.Afip => Afip.Form,
                FormType.Test => !_formOptions.Value.AllowTestForms ? throw new StaticFormNotFoundException(formType) : Test.Form,
                _ => throw new StaticFormNotFoundException(formType)
            };
        }

        public Page GetPage(FormType formType, StaticKey key)
        {
            if (!_formPages.ContainsKey(formType))
                PopulateFormDict(formType);

            if (!_formPages.TryGetValue(formType, out var formPages) || !formPages.TryGetValue(key, out var page))
                throw new KeyNotFoundException();

            return page;
        }

        public Task GetTask(FormType formType, StaticKey key)
        {
            if (!_tasks.ContainsKey(formType))
                PopulateTaskDict(formType);
            
            if (!_tasks.TryGetValue(formType, out var formTasks) || !formTasks.TryGetValue(key, out var task))
                throw new KeyNotFoundException();

            return task;
        }

        public SubTask GetSubTask(FormType formType, StaticKey key)
        {
            if (!_subTasks.ContainsKey(formType))
                PopulateFormDict(formType);
            
            if (!_subTasks.TryGetValue(formType, out var formTasks) || !formTasks.TryGetValue(key, out var task))
                throw new KeyNotFoundException();

            return task;
        }

        public string GetFormName(FormType formType)
        {
            if (!_formNames.ContainsKey(formType))
            {
                var form = GetForm(formType);
                _formNames.Add(formType, form.Name);            
            }

            return _formNames[formType];
        }

        public ContentPage GetTermsAndConditions(FormType formType)
        {
            var staticForm = GetForm(formType);

            return staticForm.TermsAndConditions;
        }

        public ContentPage GetStartPage(FormType formType)
        {
            var staticForm = GetForm(formType);

            return staticForm.StartPage;
        }

        public List<string> GetTaskGroups(FormType formType)
        {
            if (!_taskGroups.ContainsKey(formType))
            {
                var form = GetForm(formType);
                _taskGroups.Add(formType, form.TaskGroups);
            }

            return _taskGroups[formType];        
        }

        private void PopulateTaskDict(FormType formType)
        {
            var form = GetForm(formType);

            var formTasks = new Dictionary<StaticKey, Task>();
            
            foreach (var task in form.Tasks)
            {
                formTasks.Add(StaticKey.ForTaskNode(task.Id), task);
            }
            
            _tasks.Add(formType, formTasks);
        }

        private void PopulateFormDict(FormType formType)
        {
            var form = GetForm(formType);
            
            var formPages = new Dictionary<StaticKey, Page>();
            var formSubTasks = new Dictionary<StaticKey, SubTask>();
            
            if (form.TaskListPage != null)
                formPages.Add(StaticKey.ForTaskList(), form.TaskListPage);
            
            foreach (var task in form.Tasks)
            {
                if (task.PreTaskPage != null)
                    formPages.Add(StaticKey.ForPreTaskPage(task.Id), task.PreTaskPage);
                
                if (task.PostTaskPage != null)
                    formPages.Add(StaticKey.ForPostTaskPage(task.Id), task.PostTaskPage);
                
                if (task.SummaryPage != null)
                    formPages.Add(StaticKey.ForTaskSummary(task.Id), task.SummaryPage);
                
                AddItems(task.Id, task.TaskItems);
            }
            
            _formPages.Add(formType, formPages);
            _subTasks.Add(formType, formSubTasks);

            void AddItems(string taskId, IEnumerable<ITaskItem> items)
            {
                foreach (var item in items)
                {
                    if (item is TaskQuestionPage taskQuestionPage)
                    {
                        formPages.Add(StaticKey.ForTaskQuestionPage(taskId, taskQuestionPage.Id), taskQuestionPage);
                        continue;
                    }

                    if (item is SubTask subTask)
                    {
                        formSubTasks.Add(StaticKey.ForSubTask(taskId, subTask.Id), subTask);
                        formPages.Add(StaticKey.ForPostSubTaskPage(taskId, subTask.Id), subTask.PostTaskPage);
                        formPages.Add(StaticKey.ForPreSubTaskPage(taskId, subTask.Id), subTask.PreTaskPage);
                        AddItems(taskId, subTask.TaskItems);
                        continue;
                    }
                    
                    if (item is TaskQuestionGhost)
                    {
                        continue;
                    }
                    
                    throw new Exception($"Expected one of {nameof(SubTask)} or {nameof(TaskQuestionPage)} or {nameof(TaskQuestionGhost)}");
                } 
            }
        }
    }
}