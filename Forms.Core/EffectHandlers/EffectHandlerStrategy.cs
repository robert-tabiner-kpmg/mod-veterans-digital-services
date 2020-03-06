
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ExternalFrameworks;
using Forms.Core.EffectHandlers.Interfaces;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.Repositories.Interfaces;
using Forms.Core.Services.Interfaces;

namespace Forms.Core.EffectHandlers
{
    public class EffectHandlerStrategy : IEffectHandlerStrategy
    {
        private readonly IEnumerable<IEffectHandler> _effectHandlers;
        private readonly IStaticFormProvider _staticFormProvider;
        private readonly IFormRepository _formRepository;
        public EffectHandlerStrategy(IEnumerable<IEffectHandler> effectHandlers, IStaticFormProvider staticFormProvider, IFormRepository formRepository)
        {
            _effectHandlers = effectHandlers;
            _staticFormProvider = staticFormProvider;
            _formRepository = formRepository;
        }

        public async Task Handle(FormType formType, string formKey, string nodeId)
        {
            var form = await _formRepository.GetForm(formKey);

            var taskQuestionPageNode = form.Nodes.FindByKey(new Key(nodeId));
            var taskQuestionPage = (TaskQuestionPageFormNode)taskQuestionPageNode.Value;
            
            var staticPage = (TaskQuestionPage)_staticFormProvider.GetPage(formType, StaticKey.ForTaskQuestionPage(taskQuestionPage.TaskId, taskQuestionPage.PageId));

            if (staticPage.Effects.Any())
            {
                foreach (var effect in staticPage.Effects)
                {
                    await _effectHandlers.First(x => x.EffectTypes == effect.EffectTypes).Handle(effect, form, taskQuestionPageNode);
                }    
            }

            await _formRepository.SaveForm(formKey, form);
        }
    }
}