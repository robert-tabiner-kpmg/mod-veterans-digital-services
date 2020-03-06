using Graph.Models;

namespace Graph.Analysis.Traversal.Event
{
    public class OnEdgeDiscoveredEvent<TKey, TValue>
    {
        public OnEdgeDiscoveredEvent(GraphNode<TKey, TValue> fromNode, GraphNode<TKey, TValue> toNode, int cost)
        {
            FromNode = fromNode;
            ToNode = toNode;
            Cost = cost;
        }

        public GraphNode<TKey, TValue> FromNode { get; }
        public GraphNode<TKey, TValue> ToNode { get; }
        public int Cost { get; }
    }

    public class OnEdgeDiscoveredResponse
    {
        public OnEdgeDiscoveredResponse(bool shouldQueue)
        {
            ShouldQueue = shouldQueue;
        }

        public bool ShouldQueue { get; set; }
    }
}