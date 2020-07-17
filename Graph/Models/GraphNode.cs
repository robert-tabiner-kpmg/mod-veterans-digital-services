using System.Collections.Generic;
using Newtonsoft.Json;

namespace Graph.Models
{
    public class GraphNode<TKey, TValue> : Node<TKey, TValue>
    {
        private List<int> _costs;

        public GraphNode() : base() { }
        public GraphNode(TKey key, TValue value) : base(key, value) { }
        public GraphNode(TKey key, TValue value, NodeList<TKey, TValue> neighbors) : base(key, value, neighbors) { }
        
        [JsonIgnore]
        public new NodeList<TKey, TValue> Neighbors => base.Neighbors ?? (base.Neighbors = new NodeList<TKey, TValue>());
        
        public List<int> Costs => _costs ??= new List<int>();
    }
}