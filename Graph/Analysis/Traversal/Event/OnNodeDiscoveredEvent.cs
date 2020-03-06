using Graph.Models;

namespace Graph.Analysis.Traversal.Event
{
    public class OnNodeDiscoveredEvent<TKey, TValue>
    {
        public OnNodeDiscoveredEvent(GraphNode<TKey, TValue> node)
        {
            Node = node;
        }

        public GraphNode<TKey, TValue> Node { get; }
    }

    public class OnNodeDiscoveredResponse
    {
        public OnNodeDiscoveredResponse(bool shouldQueue)
        {
            ShouldQueue = shouldQueue;
        }

        public bool ShouldQueue { get; set; }
    }
}