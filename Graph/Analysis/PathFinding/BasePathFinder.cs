using System;
using Graph.Analysis.Interfaces;
using Graph.Analysis.PathFinding.Event;
using Graph.Models;

namespace Graph.Analysis.PathFinding
{
    public abstract class BasePathFinder<TKey, TValue> : IPathFinder<TKey, TValue>
    {
        public PathFindResult<TKey, TValue> FindPath(Graph<TKey, TValue> graph, GraphNode<TKey, TValue> source, GraphNode<TKey, TValue> destination)
            => FindPathInternal(graph, source, destination);
        
        protected abstract PathFindResult<TKey, TValue> FindPathInternal(Graph<TKey, TValue> graph, GraphNode<TKey, TValue> source, GraphNode<TKey, TValue> destination);
        
        internal Action<OnNodeDequeuedEvent<TKey, TValue>, OnNodeDequeuedResponse> OnNodeDequeued;
        internal Action<OnEdgeDiscoveredEvent<TKey, TValue>, OnEdgeDiscoveredResponse> OnEdgeDiscovered;

        protected OnNodeDequeuedResponse OnNodeDequeuedHandler(OnNodeDequeuedEvent<TKey, TValue> e)
        {
            var result = new OnNodeDequeuedResponse(skip: false);
            OnNodeDequeued?.Invoke(e, result);
            return result;
        }
        
        protected OnEdgeDiscoveredResponse OnEdgeDiscoveredHandler(OnEdgeDiscoveredEvent<TKey, TValue> e)
        {
            var result = new OnEdgeDiscoveredResponse(shouldQueue: true);
            OnEdgeDiscovered?.Invoke(e, result);
            return result;
        }
    }
}