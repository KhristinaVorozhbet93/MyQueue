using System.Collections;

namespace Queues
{
    public class MyQueue<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;
        private int _count;

        public int Count => _count;
        public void Enqueue(T item)
        {
            var node = new Node<T>(item);

            if (_head is null)
            {
                _head = node;
            }
            else
            {
                _tail!.Next = node; 
            }
            _tail = node;
            _count++;
        }
        public T Dequeue()
        {
            if (_head is not null)
            {
                var item = _head.Next!.Data;
                _head = _head.Next;

                if (_head is null)
                {
                    _tail = null;
                    _count = 0;
                }
                _count--;
                return item;
            }

            throw new InvalidOperationException("Queue is empty.");
        }
        public T Peek()
        {
            if (_head is not null)
            {
                var item = _head.Next!.Data;
                return item;
            }
            throw new InvalidOperationException("Queue is empty.");
        }
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }      
        public bool Contains(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_head is null)
            {
                return false;
            }

            Node<T>? current = _head;
            while (current != null && current.Data != null)
            {
                if (current.Data.Equals(item)) return true;
                current = current.Next;
            }
            return false;

        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("Index out of range.");

            Node<T>? current = _head;
            while (current != null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T>? current = _head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

