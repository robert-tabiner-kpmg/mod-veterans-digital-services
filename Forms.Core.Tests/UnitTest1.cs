using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms.Core.Forms;
using Forms.Core.Models.InFlight;
using Forms.Core.Models.InFlight.Decision;
using Forms.Core.Models.InFlight.Physical;
using Forms.Core.Models.Pages;
using Forms.Core.Repositories.Interfaces;
using Forms.Core.Services;
using Forms.Core.Services.Interfaces;
using Graph;
using Graph.Analysis;
using Graph.Analysis.PathFinding;
using Graph.Analysis.Traversal;
using Graph.Analysis.Traversal.Event;
using Moq;
using Xunit;

namespace Forms.Core.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task PathFindingTest()
        {
            var repo = new Mock<IFormRepository>();
            var provider = new Mock<IStaticFormProvider>();
            var service = new FormService(repo.Object, provider.Object);
            var form = await service.InitialiseForm("test", FormType.Test);

            var fromNode = form.Nodes.FindByKey(Key.ForTaskList());
            var toNode = form.Nodes.FindByKey(Key.ForPostTaskPage("last-task"));

            var stopKey = Key.ForTaskList();

            var pathFinder = new GraphPathFinderBuilder<Key, FormNode>(PathFinderType.PriorityPathFinder)
                .WithOnEdgeDiscoveredHandler((e, output) =>
                {
                    if (e.ToNode.Key.Equals(stopKey)) output.ShouldQueue = false;
                })
                .Build();

            var result = pathFinder.FindPath(form, fromNode, toNode);

            Assert.Equal(18, result.Cost);
        }
        
        
        [Fact]
        public async Task TraversalTest()
        {
            var repo = new Mock<IFormRepository>();
            var provider = new Mock<IStaticFormProvider>();
            var service = new FormService(repo.Object, provider.Object);
            var form = await service.InitialiseForm("test", FormType.Test);
            var taskNodeToDebug = form.Nodes.FindByKey(Key.ForDecisionTaskRouter("last-task"));
            List<string> relationships = new List<string>();

            var traversal = new GraphTraversalBuilder<Key, FormNode>(TraversalType.BreadthFirstSearch, true)
                .WithOnNodeDequeuedHandler((props, o) =>
                {
                    if (props.Node.Value is TaskListFormNode) o.Skip = true;
                })
                .WithOnEdgeDiscoveredHandler((props, o) =>
                {
                    if (props.Cost == 1)
                        relationships.Add($"{GetFriendlyNodeNameForType(props.FromNode.Value)} {GetFriendlyNodeNameForType(props.ToNode.Value)}");

                    if (props.Cost > 1)
                        o.ShouldQueue = false;
                })
                .Build();

            var result = traversal.Run(taskNodeToDebug);
            
            var builder = new StringBuilder();

            foreach (var r in relationships)
            {
                builder.Append(r);
                builder.AppendLine();
            }

            var output = builder.ToString();
        }
        
        /*
         * We can use the test below to visually debug graphs at: https://csacademy.com/app/graph_editor/
         * Debug the test and inspect the output variable at the end of the method execution
         * Past the string into the LHS of the website above and click Directed to view the task
         */
        [Fact]
        public async Task DebugTask()
        {
            var repo = new Mock<IFormRepository>();
            var provider = new Mock<IStaticFormProvider>();
            var service = new FormService(repo.Object, provider.Object);
            var form = await service.InitialiseForm("test", FormType.Test);
            List<string> relationships = new List<string>();
//            var taskNodeToDebug = form.Nodes.FindByKey(Key.ForDecisionTaskRouter("last-task"));
//            var nodeQueue = new Queue<GraphNode<Key, FormNode>>();
//            nodeQueue.Enqueue(taskNodeToDebug);
//            var discoveredItemKeys = new HashSet<string>();
//            while (nodeQueue.Count > 0)
//            {
//                var currentNode = nodeQueue.Dequeue();
//                if (currentNode is null)
//                {
//                    
//                }
//                
//                if (currentNode.Value is TaskListFormNode) continue;
//                relationships.Add(GetFriendlyNodeNameForType(currentNode.Value));
//
//                foreach (var curNode in currentNode.Neighbors)
//                {
//                    if (curNode?.Value is null || currentNode?.Value is null)
//                    {
//                        Console.WriteLine();
//                    }
//                    relationships.Add($"{GetFriendlyNodeNameForType(currentNode.Value)} {GetFriendlyNodeNameForType(curNode.Value)}");
//                    
//                    if (discoveredItemKeys.Contains(curNode.Key.Value)) continue;
//                    discoveredItemKeys.Add(curNode.Key.Value);
//                    nodeQueue.Enqueue(curNode);
//                }
//            }

            foreach (var node in form.Nodes)
            {
                relationships.Add(GetFriendlyNodeNameForType(node.Value));
                foreach (var neighbor in node.Neighbors)
                {
                    relationships.Add($"{GetFriendlyNodeNameForType(node.Value)} {GetFriendlyNodeNameForType(neighbor.Value)}");
                }
            }

            var builder = new StringBuilder();

            foreach (var r in relationships)
            {
                builder.Append(r);
                builder.AppendLine();
            }

            var output = builder.ToString();
        }

        private string GetFriendlyNodeNameForType(FormNode node)
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