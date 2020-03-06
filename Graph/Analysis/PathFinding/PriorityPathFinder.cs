using System;
using System.Collections.Generic;
using System.Linq;
using Graph.Analysis.PathFinding.Event;
using Graph.Models;

namespace Graph.Analysis.PathFinding
{
    /*
     * A simple path finding class for finding the shortest path between two nodes based on Dijkstra
     * We use a simple priority queue to ensure we are always iterating over the current shortest path
     */
    internal class PriorityPathFinder<TKey, TValue> : BasePathFinder<TKey, TValue>
    {
        protected override PathFindResult<TKey, TValue> FindPathInternal(Graph<TKey, TValue> graph, GraphNode<TKey, TValue> source, GraphNode<TKey, TValue> destination)
        {
            /*
             * We build dictionary containing every node. To begin with every distance is set to infinity, every time
             * we discover a path to a node that is less than what we currently have in the dictionary we overwrite this
             * value. We also hold the key of the previous node in the graph that resulted in the path. This means we can
             * iterate backwards from the destination node and follow the keys to find the shortest path
             */
            var distanceDictionary = graph.Nodes.ToDictionary(x => x.Key, x => (distance: double.PositiveInfinity, prev: x));
            SetDistance(source.Key, 0, distanceDictionary);
            var visited = new HashSet<TKey>();
            var heap = new PriorityQueue<double, GraphNode<TKey, TValue>>();
            heap.Enqueue(source, 0);
            
            while (!heap.IsEmpty())
            {
                var (currentCost, currentNode) = heap.Dequeue();

                if (visited.Contains(currentNode.Key)) continue;

                var onNodeDequeuedResponse = OnNodeDequeuedHandler(new OnNodeDequeuedEvent<TKey, TValue>(currentNode));
                if (onNodeDequeuedResponse.Skip) continue;

                for (var i = 0; i < currentNode.Neighbors.Count; i++)
                {
                    var neighbour = currentNode.Neighbors[i];
                    if (visited.Contains(neighbour.Key)) continue;

                    var neighbourCost = currentNode.Costs[i];
                    var onEdgeDiscoveredResponse = OnEdgeDiscoveredHandler(new OnEdgeDiscoveredEvent<TKey, TValue>(currentNode, neighbour, neighbourCost));
                    if (!onEdgeDiscoveredResponse.ShouldQueue) continue;

                    var fullNeighbourDistance = currentCost + neighbourCost;

                    if (fullNeighbourDistance < distanceDictionary[neighbour.Key].distance)
                    {
                        distanceDictionary[neighbour.Key] = (fullNeighbourDistance, currentNode);
                        heap.Enqueue(neighbour, fullNeighbourDistance);
                    }
                }

                visited.Add(currentNode.Key);
            }

            return new PathFindResult<TKey, TValue>(distanceDictionary[destination.Key].distance,
                FindPath(distanceDictionary, source, destination));
        }

        private static IList<GraphNode<TKey, TValue>> FindPath(
            IReadOnlyDictionary<TKey, (double cost, GraphNode<TKey, TValue> value)> distanceDictionary,
            GraphNode<TKey, TValue> source, GraphNode<TKey, TValue> destination)
        {
            var path = new List<GraphNode<TKey, TValue>> {destination};

            var currentKey = destination.Key;
            while (true)
            {
                if (currentKey.Equals(source.Key))
                    break;

                var (distance, prevNode) = distanceDictionary[currentKey];

                if (double.IsPositiveInfinity(distance))
                    throw new Exception("No path found between source and destination");

                path.Add(prevNode);
                currentKey = prevNode.Key;
            }

            path.Reverse();

            return path;
        }

        private static void SetDistance(TKey key, int distance, IReadOnlyDictionary<TKey, (double distance, GraphNode<TKey, TValue> prev)> distanceDictionary)
        {
            if (!distanceDictionary.TryGetValue(key, out var sourceValue)) throw new KeyNotFoundException();

            sourceValue.distance = distance;
        }
    }
}