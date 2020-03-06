using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Static;
using Graph.Models;
using Task = System.Threading.Tasks.Task;

namespace Forms.Core.Services.Interfaces
{
    public interface IFormService
    {
        Task<Graph<Key, FormNode>> InitialiseForm(string formKey, FormType formType);
        Task SavePage(string formKey, InFlightPage page);
        Task<string> RepeatTask(FormType fromType, string formKey, string taskId);
        Task<string> RepeatSubTask(FormType formType, string formKey, string nodeId);
        Task<string> DeleteTaskItem(FormType formType, string formKey, string nodeId, string taskItemNodeId);
        Task<string> DeleteSubTaskItem(FormType formType, string formKey, string subTaskNode, string subTaskItemNode);
        Task<string> GetPreviousNode(string formKey, string currentNodeId);
        Task<GraphNode<Key, FormNode>> GetFormNode(string formKey, string nodeId);
        Task<(bool isValid, IDictionary<string, string> errors)> ValidateQuestionPage(FormType formType, string formKey, InFlightPage inFlightPage);
        Task<(int questionNumber, int totalQuestions)> GetQuestionNumber(string formKey, string questionPageNodeId);
    }
}