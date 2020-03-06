namespace Graph.Models
{
    public class Graph<TKey, TValue>
    {
        public NodeList<TKey, TValue> Nodes { get; }

        public int Count => Nodes.Count;
        
        public Graph() : this(null) {}
        public Graph(NodeList<TKey, TValue> nodeSet)
        {
            if (nodeSet == null)
                this.Nodes = new NodeList<TKey, TValue>();
            else
                this.Nodes = nodeSet;
        }

        public void AddNode(GraphNode<TKey, TValue> node)
        {
            Nodes.Add(node);
        }

        public void AddNode(TKey key, TValue value)
        {
            Nodes.Add(new GraphNode<TKey, TValue>(key, value));
        }

        public GraphNode<TKey, TValue> GetGraphNode(TKey key)
        {
            return Nodes.FindByKey(key);
        }

        public void AddDirectedEdge(GraphNode<TKey, TValue> from, GraphNode<TKey, TValue> to, int cost = 1)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        public void AddUndirectedEdge(GraphNode<TKey, TValue> from, GraphNode<TKey, TValue> to, int cost = 1)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }

        public void RemoveEdge(GraphNode<TKey, TValue> from, GraphNode<TKey, TValue> to)
        {
            from.Costs.RemoveAt(from.Neighbors.IndexOf(to));
            from.Neighbors.Remove(to);
        }

        public bool Contains(TValue value)
        {
            return Nodes.FindByValue(value) != null;
        }

        public bool Contains(TKey key)
        {
            return Nodes.FindByKey(key) != null;
        }

        public bool Remove(TValue value)
        {
            var nodeToRemove = (GraphNode<TKey, TValue>) Nodes.FindByValue(value);
            return Remove(nodeToRemove);
        }

        public bool Remove(TKey key)
        {
            var nodeToRemove = (GraphNode<TKey, TValue>) Nodes.FindByKey(key);
            return Remove(nodeToRemove);
        }

        public bool Remove(GraphNode<TKey, TValue> nodeToRemove)
        {
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            Nodes.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (var node1 in Nodes)
            {
                var node = (GraphNode<TKey, TValue>) node1;
                var index = node.Neighbors.IndexOf(nodeToRemove);

                if (index == -1) continue;

                // remove the reference to the node and associated cost
                node.Neighbors.RemoveAt(index);
                node.Costs.RemoveAt(index);
            }

            return true;
        }
    }
}