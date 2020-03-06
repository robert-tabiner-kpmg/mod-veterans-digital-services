using System.Threading.Tasks;
using Forms.Core.Forms;

namespace Forms.Core.EffectHandlers.Interfaces
{
    public interface IEffectHandlerStrategy
    {
        Task Handle(FormType formType, string formKey, string nodeId);
    }
}