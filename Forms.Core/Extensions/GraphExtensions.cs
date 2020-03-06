using System;
using System.Collections.Generic;
using System.Text;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Decision.Ghost;
using Forms.Core.Models.InFlight.Physical;
using Graph;
using Graph.Analysis;
using Graph.Analysis.Traversal;
using Graph.Analysis.Traversal.Event;
using Graph.Models;

namespace Forms.Core.Extensions
{
    public static class GraphExtensions
    {
        /// <summary>
        /// Used for debugging purposes to set the structure of the graph from a given node
        /// </summary>
        /// <param name="graphNode"></param>
        /// <returns>
        /// A string containing a friendly-named graph structure which can be copied and visualised
        /// using the following URL: https://csacademy.com/app/graph_editor/
        /// </returns>
        public static string GetGraphOutput(this GraphNode<Key, FormNode> graphNode)
        {
            List<string> relationships = new List<string>();
            var nodeQueue = new Queue<GraphNode<Key, FormNode>>();
            nodeQueue.Enqueue(graphNode);
            var discoveredItemKeys = new HashSet<string>();
            while (nodeQueue.Count > 0)
            {
                var currentNode = nodeQueue.Dequeue();
                
                if (currentNode.Value is TaskListFormNode) continue;

                foreach (var curNode in currentNode.Neighbors)
                {
                    relationships.Add($"{GetFriendlyNodeNameForType(currentNode.Value)} {GetFriendlyNodeNameForType(curNode.Value)}");
                    
                    if (discoveredItemKeys.Contains(curNode.Key.Value)) continue;
                    discoveredItemKeys.Add(curNode.Key.Value);
                    nodeQueue.Enqueue(curNode);
                }
            }

            var builder = new StringBuilder();

            foreach (var r in relationships)
            {
                builder.Append(r);
                builder.AppendLine();
            }

            var output = builder.ToString();

            return output;
        }
        
        private static string GetFriendlyNodeNameForType(FormNode node)
        {
            switch (node)
            {
                case FormRouterFormNode frnfn:
                    return "ROUTER";
                case TaskQuestionPageFormNode tqp:
                    return tqp.PageId;
                case PreTaskFormNode prefn:
                    return "PRE";
                case PostTaskFormNode postfn:
                    return "POST";
                case TaskSummaryFormNode sumfn:
                    return "SUM";
                case TaskRouterFormNode trfn:
                    return "TASK";
                case TaskItemRouterFormNode tirfn:
                    return "ITEM" + tirfn.RepeatIndex;
                case TaskListFormNode tlfn:
                    return "TASKLIST";
                case SubTaskRouterFormNode strfn:
                    return strfn.SubTaskId;
                case SubTaskItemRouterFormNode stirfn:
                    return stirfn.SubTaskId + "-ITEM";
                case PreSubTaskFormNode pstfn:
                    return pstfn.SubTaskId + "-PRE";
                case PostSubTaskFormNode poststfn:
                    return poststfn.SubTaskId + "-POST";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}