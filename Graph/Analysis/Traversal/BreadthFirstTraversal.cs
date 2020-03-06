using System.Collections.Generic;
using System.Linq;
using Graph.Analysis.Traversal.Event;
using Graph.Models;

namespace Graph.Analysis.Traversal
{
    /*
     * Simple traversal algorithm, we traverse nodes and keep adding any neighbours to the queue. If we have already
     * discovered a neighbour we can ignore it. We provide a few simple callbacks in this function which allow for
     * customising the behaviour.
     * 1 - OnNodeDequeued: Called when removing a node from the queue
     * 2 - OnEdgeDiscovered: Called when finding an edge between the current node and another node
     * 3 - OnNodeDiscovered: Called when an edge resulted in finding an undiscovered node
     */
    internal class BreadthFirstTraversal<TKey, TValue> : BaseTraversal<TKey, TValue>
    {
        internal BreadthFirstTraversal(bool recordTraversal)
        {
            RecordTraversal = recordTraversal;
        }
        
        protected override TraversalResult<TKey, TValue> RunInternal(GraphNode<TKey, TValue> entryPoint)
        {
            var discovered = new HashSet<TKey>();
            var queue = new Queue<(GraphNode<TKey, TValue> node, int depth)>();
            queue.Enqueue((entryPoint, 0));

            while (queue.Any())
            {
                var (node, depth) = queue.Dequeue();

                if (RecordTraversal) AllTraversedNodes.Add((node, depth));
                
                var dequeueResponse = OnNodeDequeuedHandler(new OnNodeDequeuedEvent<TKey, TValue>(node));

                if (dequeueResponse.IsComplete) return OnComplete(node);
                if (dequeueResponse.Skip) continue;

                for (var i = 0; i < node.Neighbors.Count; i++)
                {
                    var neighbour = node.Neighbors[i];
                    var cost = node.Costs[i];

                    var onEdgeDiscoveredResponse = OnEdgeDiscoveredHandler(new OnEdgeDiscoveredEvent<TKey, TValue>(node, neighbour, cost));

                    if (!onEdgeDiscoveredResponse.ShouldQueue) continue;
                    if (discovered.Contains(neighbour.Key)) continue;
                    
                    discovered.Add(neighbour.Key);

                    var onNodeDiscoveredResponse = OnNodeDiscoveredHandler(new OnNodeDiscoveredEvent<TKey, TValue>(neighbour));
                    if (!onNodeDiscoveredResponse.ShouldQueue) continue;

                    queue.Enqueue((neighbour, depth + 1));
                }
            }
            return OnComplete(null);
        }
    }
}