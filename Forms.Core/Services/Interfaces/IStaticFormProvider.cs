using System.Collections.Generic;
using System.Threading.Tasks;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.Pages;
using Forms.Core.Models.Static;
using Task = Forms.Core.Models.Static.Task;

namespace Forms.Core.Services.Interfaces
{
    public interface IStaticFormProvider
    {
        Form GetForm(FormType formType);
        Page GetPage(FormType formType, StaticKey key);
        Task GetTask(FormType formType, StaticKey key);
        SubTask GetSubTask(FormType formType, StaticKey key);
        string GetFormName(FormType formType);
        ContentPage GetTermsAndConditions(FormType formType);
        ContentPage GetStartPage(FormType formType);
        List<string> GetTaskGroups(FormType formType);
    }
}