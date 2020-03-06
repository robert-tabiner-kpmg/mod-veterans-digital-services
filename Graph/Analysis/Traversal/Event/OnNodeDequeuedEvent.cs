using Graph.Models;

namespace Graph.Analysis.Traversal.Event
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
        public OnNodeDequeuedResponse(bool skip, bool isComplete)
        {
            Skip = skip;
            IsComplete = isComplete;
        }

        public bool Skip { get; set; }
        public bool IsComplete { get; set; }
    }
}