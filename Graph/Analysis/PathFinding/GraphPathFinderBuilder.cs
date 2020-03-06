using System;
using Graph.Analysis.Interfaces;
using Graph.Analysis.PathFinding.Event;

namespace Graph.Analysis.PathFinding
{
    public class GraphPathFinderBuilder<TKey, TValue>
    {
        private BasePathFinder<TKey, TValue> Result { get; }
        
        public GraphPathFinderBuilder(PathFinderType pathFinderType)
        {
            Result = pathFinderType switch
            {
                PathFinderType.PriorityPathFinder => new PriorityPathFinder<TKey, TValue>(),
                _ => throw new NotImplementedException()
            };
        }

        public GraphPathFinderBuilder<TKey, TValue> WithOnNodeDequeuedHandler(Action<OnNodeDequeuedEvent<TKey, TValue>, OnNodeDequeuedResponse> func)
        {
            Result.OnNodeDequeued = func;
            return this;
        }

        public GraphPathFinderBuilder<TKey, TValue> WithOnEdgeDiscoveredHandler(Action<OnEdgeDiscoveredEvent<TKey, TValue>, OnEdgeDiscoveredResponse> func)
        {
            Result.OnEdgeDiscovered = func;
            return this;
        }

        public IPathFinder<TKey, TValue> Build()
        {
            return Result;
        }
    }
}