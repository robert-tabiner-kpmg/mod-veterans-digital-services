using System.Linq;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;
using Graph.Models;

namespace Forms.Core.Extensions
{
    public static class GraphNodePredicates
    {
        public static bool IsTaskListNode(GraphNode<Key, FormNode> node)
        {
            return !node.Value.IsDecisionNode && node.Value is PhysicalFormNode pfn &&
                   pfn.PhysicalFormNodeType == PhysicalFormNodeType.TaskList;
        }
        
        public static bool IsAnyTaskListNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode
                ? node.Value is DecisionFormNode dfn && dfn.DecisionFormNodeType == DecisionFormNodeType.TaskListGhost
                : node.Value is PhysicalFormNode pfn && pfn.PhysicalFormNodeType == PhysicalFormNodeType.TaskList;
        }
        
        public static bool IsTaskRouterNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode && node.Value is DecisionFormNode dfn &&
                   dfn.DecisionFormNodeType == DecisionFormNodeType.TaskRouter;
        }
        
        public static bool IsTaskItemRouterNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode && node.Value is DecisionFormNode dfn &&
                   dfn.DecisionFormNodeType == DecisionFormNodeType.TaskItemRouter;
        }

        public static bool IsAnyPreTaskNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode
                ? node.Value is DecisionFormNode dfn && dfn.DecisionFormNodeType == DecisionFormNodeType.PreTaskGhost
                : node.Value is PhysicalFormNode pfn && pfn.PhysicalFormNodeType == PhysicalFormNodeType.PreTask;
        }
        
        public static bool IsAnyPostTaskNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode
                ? node.Value is DecisionFormNode dfm && dfm.DecisionFormNodeType == DecisionFormNodeType.PostTaskGhost
                : node.Value is PhysicalFormNode pfm && pfm.PhysicalFormNodeType == PhysicalFormNodeType.PostTask;
        }

        public static bool IsAnyTaskSummaryNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode
                ? node.Value is DecisionFormNode dfn && dfn.DecisionFormNodeType == DecisionFormNodeType.SummaryGhost
                : node.Value is PhysicalFormNode pfn && pfn.PhysicalFormNodeType == PhysicalFormNodeType.TaskSummary;
        }

        public static bool IsAnyTaskItemNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode
                ? node.Value is DecisionFormNode dfn &&
                  (dfn.DecisionFormNodeType == DecisionFormNodeType.SubTaskRouter || dfn.DecisionFormNodeType == DecisionFormNodeType.TaskQuestionGhost)
                : node.Value is PhysicalFormNode pfn &&
                  pfn.PhysicalFormNodeType == PhysicalFormNodeType.TaskQuestionPage;
        }

        public static bool IsTaskQuestionPageNode(GraphNode<Key, FormNode> node)
        {
            return !node.Value.IsDecisionNode && node.Value is PhysicalFormNode pfn &&
                   pfn.PhysicalFormNodeType == PhysicalFormNodeType.TaskQuestionPage;
        }
        
        public static bool IsTaskQuestionGhost(GraphNode<Key, FormNode> node)
        {
            return !node.Value.IsDecisionNode && node.Value is DecisionFormNode dfn &&
                   dfn.DecisionFormNodeType == DecisionFormNodeType.TaskQuestionGhost;
        }

        public static bool IsSubTaskRouterNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode && node.Value is DecisionFormNode dfn &&
                   dfn.DecisionFormNodeType == DecisionFormNodeType.SubTaskRouter;
        }

        public static bool IsSubTaskItemRouterNode(GraphNode<Key, FormNode> node)
        {
            return node.Value.IsDecisionNode && node.Value is DecisionFormNode dfn &&
                   dfn.DecisionFormNodeType == DecisionFormNodeType.SubTaskItemRouter;
        }

        public static bool IsPreSubTaskNode(GraphNode<Key, FormNode> node)
        {
            return !node.Value.IsDecisionNode && node.Value is PhysicalFormNode pfn &&
                   pfn.PhysicalFormNodeType == PhysicalFormNodeType.PreSubTask;
        }

        public static bool IsPostSubTaskNode(GraphNode<Key, FormNode> node)
        {
            return !node.Value.IsDecisionNode && node.Value is PhysicalFormNode pfn &&
                   pfn.PhysicalFormNodeType == PhysicalFormNodeType.PostSubTask;
        }
    }
}