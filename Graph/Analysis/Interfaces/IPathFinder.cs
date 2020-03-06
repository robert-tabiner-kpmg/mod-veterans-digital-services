using Graph.Analysis.PathFinding;
using Graph.Models;

namespace Graph.Analysis.Interfaces
{
    public interface IPathFinder<TKey, TValue>
    {
        /// <summary>
        /// Find the shortest path between two nodes, source and destination
        /// </summary>
        /// <param name="graph">Parent graph of the two nodes</param>
        /// <param name="source">Node to begin the search</param>
        /// <param name="destination">Node to end the search</param>
        /// <returns>A path and cost between the two provided nodes</returns>
        PathFindResult<TKey, TValue> FindPath(Graph<TKey, TValue> graph, GraphNode<TKey, TValue> source, GraphNode<TKey, TValue> destination);
    }
}