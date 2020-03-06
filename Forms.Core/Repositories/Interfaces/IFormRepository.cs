using System.Threading.Tasks;
using Forms.Core.Models.InFlight;
using Graph.Models;

namespace Forms.Core.Repositories.Interfaces
{
    public interface IFormRepository
    {
        Task<Graph<Key, FormNode>> GetForm(string formKey);
        Task SaveForm(string formKey, Graph<Key, FormNode> form);
    }
}