using System.Collections.ObjectModel;

namespace Graph.Models
{
    public class NodeList<TKey, TValue> : Collection<GraphNode<TKey, TValue>>
    {
        public NodeList() : base() { }

        public NodeList(int initialSize)
        {
            // Add the specified number of items
            for (int i = 0; i < initialSize; i++)
                base.Items.Add(default(GraphNode<TKey, TValue>));
        }

        public GraphNode<TKey, TValue> FindByValue(TValue value)
        {
            // search the list for the value
            foreach (GraphNode<TKey, TValue> node in Items)
                if (node.Value.Equals(value))
                    return node;

            // if we reached here, we didn't find a matching node
            return null;
        }
        
        public GraphNode<TKey, TValue> FindByKey(TKey key)
        {
            // search the list for the value
            foreach (GraphNode<TKey, TValue> node in Items)
                if (node.Key.Equals(key))
                    return node;

            // if we reached here, we didn't find a matching node
            return null;
        }
    }
}