namespace Graph.Models
{
    public class Node<TKey, TValue>
    {
        public Node() {}

        public Node(TKey key, TValue value, NodeList<TKey, TValue> neighbors = null)
        {
            Key = key;
            Value = value;
            Neighbors = neighbors;
        }

        public TValue Value { get; set; }
        public TKey Key { get; set; }

        public NodeList<TKey, TValue> Neighbors { get; set; }
    }
}