using System;
using System.Linq;
using System.Threading.Tasks;
using Common.ExternalFrameworks;
using Forms.Core.EffectHandlers.Interfaces;
using Forms.Core.EffectHandlers.Models;
using Forms.Core.Extensions;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Physical;
using Graph.Models;

namespace Forms.Core.EffectHandlers.Handlers
{
    public class PathChangeEffectHandler : IEffectHandler
    {
        public EffectTypes EffectTypes => EffectTypes.PathChange;

        public Task Handle(Effect effect, Graph<Key, FormNode> form, GraphNode<Key, FormNode> currentPageNode)
        {
            if (!(effect is PathChangeEffect pathChangeEffect)) throw new ArgumentException();
            var taskQuestionPage = currentPageNode.AssertType<TaskQuestionPageFormNode>();

            var newPath = pathChangeEffect.Handle(taskQuestionPage.Questions);

            var newKey = Key.ForTaskItemPage(taskQuestionPage.TaskId, newPath, taskQuestionPage.RepeatIndices);
            
            if (currentPageNode.Neighbors.Any(x => x.Key.Equals(newKey)))
                return Task.CompletedTask;

            var newNeighbour = form.Nodes.FindByKey(newKey);
            var currentNeighbour = currentPageNode.Neighbors.First();
            
            form.RemoveEdge(currentPageNode, currentNeighbour);
            form.AddDirectedEdge(currentPageNode, newNeighbour);

            return Task.CompletedTask;
        }
    }
}