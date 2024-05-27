using System.Collections;

namespace Queues
{
    public class MyQueue<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;
        private int _count;
        private object _locker = new();

        public int Count => _count;

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item);

            while (true)
            {
                var currentTail = _tail;

                if (currentTail == null)
                {
                    if (Interlocked.CompareExchange(ref _tail, newNode, null) == null)
                    {
                        Interlocked.CompareExchange(ref _head, newNode, null);
                        Interlocked.Increment(ref _count);
                        return;
                    }
                }
                else
                {
                    newNode.Next = currentTail.Next;

                    if (Interlocked.CompareExchange(ref _tail, newNode, currentTail) == currentTail)
                    {
                        Interlocked.Increment(ref _count);
                        return;
                    }
                }
            }
        }

        public T Dequeue()
        {
            while (true)
            {
                var currentHead = _head;

                if (currentHead == null)
                {
                    throw new InvalidOperationException("Queue is empty.");
                }

                var nextNode = currentHead.Next;

                if (Interlocked.CompareExchange(ref _head, nextNode, currentHead) == currentHead)
                {
                    if (nextNode != null)
                    {
                        Interlocked.Decrement(ref _count);
                        return nextNode.Data;
                    }
                    else
                    {
                        Interlocked.Exchange(ref _tail, null);
                        Interlocked.Decrement(ref _count);
                        return currentHead.Data;
                    }
                }
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _head = null;
                _tail = null;
                _count = 0;
            }
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

            lock (_locker)
            {
                Node<T>? current = _head;
                while (current != null && current.Data != null)
                {
                    if (current.Data.Equals(item)) return true;
                    current = current.Next;
                }
            }
            return false;

        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("Index out of range.");

            lock (_locker)
            {
                Node<T>? current = _head;
                while (current != null)
                {

                    array[arrayIndex++] = current.Data;
                    current = current.Next;
                }
            }
        }

        public T Peek()
        {
            if (_head is not null)
            {
                lock (_locker)
                {
                    var item = _head.Next!.Data;
                    return item;
                }
            }

            throw new InvalidOperationException("Queue is empty.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_locker)
            {
                Node<T>? current = _head;

                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

