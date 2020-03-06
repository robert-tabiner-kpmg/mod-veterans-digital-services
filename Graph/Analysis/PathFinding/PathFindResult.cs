using System.Collections.Generic;
using Graph.Models;

namespace Graph.Analysis.PathFinding
{
    public class PathFindResult<TKey, TValue>
    {
        public PathFindResult(double cost, IList<GraphNode<TKey, TValue>> path)
        {
            Cost = cost;
            Path = path;
        }

        public double Cost { get; }
        public IList<GraphNode<TKey, TValue>> Path { get; }
    }
}