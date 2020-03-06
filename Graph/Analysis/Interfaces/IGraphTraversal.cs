using Graph.Analysis.Traversal;
using Graph.Models;

namespace Graph.Analysis.Interfaces
{
    public interface IGraphTraversal<TKey, TValue>
    {
        /// <summary>
        /// Find all discoverable nodes from the entry-point
        /// </summary>
        /// <param name="entryPoint">Node to begin our search</param>
        /// <returns>Destination node if required, and a list of traversed nodes and depths</returns>
        TraversalResult<TKey, TValue> Run(GraphNode<TKey, TValue> entryPoint);
    }
}