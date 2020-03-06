using System;
using System.Collections.Generic;
using Graph.Analysis.Interfaces;
using Graph.Analysis.Traversal.Event;
using Graph.Models;

namespace Graph.Analysis.Traversal
{
    public abstract class BaseTraversal<TKey, TValue> : IGraphTraversal<TKey, TValue>
    {
        protected bool RecordTraversal { get; set; }
        
        protected List<(GraphNode<TKey, TValue> node, int depth)> AllTraversedNodes { get; set; } =
            new List<(GraphNode<TKey, TValue> node, int depth)>();
        
        internal Action<OnNodeDequeuedEvent<TKey, TValue>, OnNodeDequeuedResponse> OnNodeDequeued;
        internal Action<OnNodeDiscoveredEvent<TKey, TValue>, OnNodeDiscoveredResponse> OnNodeDiscovered;
        internal Action<OnEdgeDiscoveredEvent<TKey, TValue>, OnEdgeDiscoveredResponse> OnEdgeDiscovered;

        protected OnNodeDequeuedResponse OnNodeDequeuedHandler(OnNodeDequeuedEvent<TKey, TValue> value)
        {
            var output = new OnNodeDequeuedResponse(skip: false, isComplete: false);
            OnNodeDequeued?.Invoke(value, output);
            return output;
        }
        
        protected OnNodeDiscoveredResponse OnNodeDiscoveredHandler(OnNodeDiscoveredEvent<TKey, TValue> value)
        {
            var output = new OnNodeDiscoveredResponse(shouldQueue: true);
            OnNodeDiscovered?.Invoke(value, output);
            return output;
        }
        
        protected OnEdgeDiscoveredResponse OnEdgeDiscoveredHandler(OnEdgeDiscoveredEvent<TKey, TValue> value)
        {
            var output = new OnEdgeDiscoveredResponse(shouldQueue:true);
            OnEdgeDiscovered?.Invoke(value, output);
            return output;
        }
        
        public TraversalResult<TKey, TValue> Run(GraphNode<TKey, TValue> entryPoint)
        {
            AllTraversedNodes = new List<(GraphNode<TKey, TValue> node, int depth)>();
            return RunInternal(entryPoint);
        }

        protected TraversalResult<TKey, TValue> OnComplete(GraphNode<TKey, TValue> resultNode)
        {
            return new TraversalResult<TKey, TValue>(resultNode, AllTraversedNodes);
        }
        
        protected abstract TraversalResult<TKey, TValue> RunInternal(GraphNode<TKey, TValue> entryPoint);
    }
}