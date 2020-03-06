using Forms.Core.Exceptions;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;
using Graph.Models;

namespace Forms.Core.Extensions
{
    public static class GraphNodeExtensions
    {
        public static T AssertType<T>(this GraphNode<Key, FormNode> node) where T : FormNode
        {
            if (!(node.Value is T value))
            {
                throw new UnexpectedNodeTypeException<T>(node.Value);
            }

            return value;
        }
        
        public static bool IsTaskListNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsTaskListNode(node);

        public static bool IsTaskListNode(this GraphNode<Key, FormNode> node, out TaskListFormNode taskListFormNode)
        {
            taskListFormNode = null;
            if (!GraphNodePredicates.IsTaskListNode(node)) return false;

            taskListFormNode = (TaskListFormNode) node.Value;
            return true;
        }

        public static bool IsTaskRouterNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsTaskRouterNode(node);
        
        public static bool IsTaskRouterNode(this GraphNode<Key, FormNode> node, out TaskRouterFormNode taskRouterFormNode)
        {
            taskRouterFormNode = null;
            if (!GraphNodePredicates.IsTaskRouterNode(node)) return false;

            taskRouterFormNode = (TaskRouterFormNode) node.Value;
            return true;
        }

        public static bool IsTaskItemRouterNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsTaskItemRouterNode(node);
        
        public static bool IsTaskItemRouterNode(this GraphNode<Key, FormNode> node, out TaskItemRouterFormNode taskItemRouterFormNode)
        {
            taskItemRouterFormNode = null;
            if (!GraphNodePredicates.IsTaskItemRouterNode(node)) return false;

            taskItemRouterFormNode = (TaskItemRouterFormNode) node.Value;
            return true;
        }

        public static bool IsTaskQuestionPageNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsTaskQuestionPageNode(node);

        public static bool IsTaskQuestionPageNode(this GraphNode<Key, FormNode> node, out TaskQuestionPageFormNode value)
        {
            value = null;
            if (!GraphNodePredicates.IsTaskQuestionPageNode(node)) return false;
            value = (TaskQuestionPageFormNode) node.Value;
            return true;
        }

        public static bool IsSubTaskRouterNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsSubTaskRouterNode(node);
        
        public static bool IsSubTaskRouterNode(this GraphNode<Key, FormNode> node, out SubTaskRouterFormNode value)
        {
            value = null;
            if (!GraphNodePredicates.IsSubTaskRouterNode(node)) return false;
            value = (SubTaskRouterFormNode) node.Value;
            return true;
        }

        public static bool IsSubTaskItemRouterNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsSubTaskItemRouterNode(node);
        
        public static bool IsSubTaskItemRouterNode(this GraphNode<Key, FormNode> node, out SubTaskItemRouterFormNode value)
        {
            value = null;
            if (!GraphNodePredicates.IsSubTaskItemRouterNode(node)) return false;
            value = (SubTaskItemRouterFormNode) node.Value;
            return true;
        }

        public static bool IsPreSubTaskNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsPreSubTaskNode(node);
        
        public static bool IsPreSubTaskNode(this GraphNode<Key, FormNode> node, out PreSubTaskFormNode value)
        {
            value = null;
            if (!GraphNodePredicates.IsPreSubTaskNode(node)) return false;
            value = (PreSubTaskFormNode) node.Value;
            return true;
        }

        public static bool IsPostSubTaskNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsPostSubTaskNode(node);
        
        public static bool IsPostSubTaskNode(this GraphNode<Key, FormNode> node, out PostSubTaskFormNode value)
        {
            value = null;
            if (!GraphNodePredicates.IsPostSubTaskNode(node)) return false;
            value = (PostSubTaskFormNode) node.Value;
            return true;
        }
        
        public static bool IsTaskQuestionGhost(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsTaskQuestionGhost(node);
        
        /*
         * We don't offer out parameter overloads for the IsAny functions below as we cannot determine the actual
         * type of the node
         */
        public static bool IsAnyPreTaskNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsAnyPreTaskNode(node);
        
        public static bool IsAnyPostTaskNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsAnyPostTaskNode(node);

        public static bool IsAnyTaskSummaryNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsAnyTaskSummaryNode(node);

        public static bool IsAnyTaskItemNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsAnyTaskItemNode(node);
        
        public static bool IsAnyTaskListNode(this GraphNode<Key, FormNode> node) =>
            GraphNodePredicates.IsAnyTaskListNode(node);
    }
}