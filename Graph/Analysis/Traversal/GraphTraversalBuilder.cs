using System;
using Graph.Analysis.Interfaces;
using Graph.Analysis.Traversal.Event;

namespace Graph.Analysis.Traversal
{
    public class GraphTraversalBuilder<TKey, TValue>
    {
        private BaseTraversal<TKey, TValue> Result { get; }
        
        public GraphTraversalBuilder(TraversalType traversalType, bool recordTraversal)
        {
            Result = traversalType switch
            {
                TraversalType.BreadthFirstSearch => new BreadthFirstTraversal<TKey, TValue>(recordTraversal),
                _ => throw new NotImplementedException()
            };
        }

        public GraphTraversalBuilder<TKey, TValue> WithOnNodeDequeuedHandler(Action<OnNodeDequeuedEvent<TKey, TValue>, OnNodeDequeuedResponse> func)
        {
            Result.OnNodeDequeued = func;
            return this;
        }

        public GraphTraversalBuilder<TKey, TValue> WithOnNodeDiscoveredHandler(Action<OnNodeDiscoveredEvent<TKey, TValue>, OnNodeDiscoveredResponse> func)
        {
            Result.OnNodeDiscovered = func;
            return this;
        }

        public GraphTraversalBuilder<TKey, TValue> WithOnEdgeDiscoveredHandler(Action<OnEdgeDiscoveredEvent<TKey, TValue>, OnEdgeDiscoveredResponse> func)
        {
            Result.OnEdgeDiscovered = func;
            return this;
        }

        public IGraphTraversal<TKey, TValue> Build()
        {
            return Result;
        }
    }
}