using System.Collections.Generic;
using Graph.Models;

namespace Graph.Analysis.Traversal
{
    public class TraversalResult<TKey, TValue>
    {
        public TraversalResult(GraphNode<TKey, TValue> result, List<(GraphNode<TKey, TValue> node, int depth)> traversalOrder)
        {
            Result = result;
            TraversalOrder = traversalOrder;
        }

        public GraphNode<TKey, TValue> Result { get; }
        public List<(GraphNode<TKey, TValue> node, int depth)> TraversalOrder { get; }
    }
}