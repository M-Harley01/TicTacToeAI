using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class HashQueue<T> : Queue<T>
    {
        private readonly HashSet<T> hashSet = [];
        public HashQueue() : base() { }

        public new T Dequeue()
        {
            T item = base.Dequeue();
            hashSet.Remove(item);
            return item;
        }

        public new void Enqueue(T item)
        {
            hashSet.Add(item);
            base.Enqueue(item);
        }

        public new bool Contains(T item)
        {
            return hashSet.Contains(item);
        }
    }
}
