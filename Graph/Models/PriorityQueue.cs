using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Models
{
    public class PriorityQueue<TKey, TValue>
    {
        private int _totalSize;
        private readonly SortedDictionary<TKey, Queue<TValue>> _storage;

        public PriorityQueue ()
        {
            _storage = new SortedDictionary<TKey, Queue<TValue>> ();
            _totalSize = 0;
        }

        public bool IsEmpty ()
        {
            return (_totalSize == 0);
        }

        public (TKey priorityKey, TValue value) Dequeue ()
        {
            if (IsEmpty ()) {
                throw new Exception ("Queue is empty");
            }

            var (priority, queue) = _storage.First();
            var nextValue = queue.Dequeue();

            if (queue.Count == 0) _storage.Remove(priority);

            _totalSize--;
            return (priority, nextValue);
        }

        public (TKey priorityKey, TValue value) Peek ()
        {
            if (IsEmpty ())
                throw new Exception ("Queue is empty");
            
            foreach ((TKey priority, Queue<TValue> queue) in _storage) {
                if (queue.Count > 0)
                    return (priority, queue.Dequeue());
            }

            return (default, default); // Should never be reached
        }

        public void Enqueue (TValue item, TKey priority)
        {
            if (!_storage.ContainsKey (priority)) {
                _storage.Add (priority, new Queue<TValue>());
            }
            _storage[priority].Enqueue (item);
            _totalSize++;
        }
    }
}