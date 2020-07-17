using System.Collections.Generic;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public NodeList<TKey, TValue> Neighbors { get; set; }
        
        public List<TKey> NeighborIds { get; set; } = new List<TKey>();
    }
}