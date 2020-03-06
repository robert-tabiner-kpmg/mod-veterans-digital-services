using Graph.Models;

namespace Graph.Analysis.PathFinding.Event
{
    public class OnNodeDequeuedEvent<TKey, TValue>
    {
        public OnNodeDequeuedEvent(GraphNode<TKey, TValue> node)
        {
            Node = node;
        }

        public GraphNode<TKey, TValue> Node { get;}
    }

    public class OnNodeDequeuedResponse
    {
        public OnNodeDequeuedResponse(bool skip)
        {
            Skip = skip;
        }

        public bool Skip { get; set; }
    }
}