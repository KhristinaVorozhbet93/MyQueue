using System.Collections;

namespace MyQueues
{
    public class MyQueue<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        private T[] _array;
        private int _size;

        public MyQueue()
        {
            _array = Array.Empty<T>();
        }
        public MyQueue(int capacity)
        {
            _array = new T[capacity];
        }
        public int Count => _size;
        public void Enqueue(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_size == _array.Length)
            {
                Grow(_size);
            }

            _array[_size] = item;
            _size++;
        }

        public T Dequeue()
        {
            if (_size != 0)
            {
                T temp = _array[0];
                for (int i = 1; i < _size; i++)
                {
                    _array[i - 1] = _array[i];
                }
                _size--;
                return temp;
            }

            throw new InvalidOperationException("Queue is empty.");
        }

        public T Peek()
        {
            if (_size != 0)
            {
                return _array[0];
            }

            throw new InvalidOperationException("Queue is empty.");
        }
        public bool Contains(T item)
        {
            if (_size == 0)
            {
                return false;
            }

            return Array.IndexOf(_array, item, 0, _size) >= 0;
        }
        public void Clear()
        {
            Array.Clear(_array, 0, _size);
            _size = 0;
            _array = Array.Empty<T>();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, 0, array, arrayIndex, _size);
        }
        public IEnumerator<T> GetEnumerator()
        {

            return new MyQueueEnumerator<T>(_array);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Grow(int capacity)
        {
            const int GrowFactor = 2;
            int newCapacity = Math.Max(GrowFactor * capacity, 1);

            T[] newArray = new T[newCapacity];
            Array.Copy(_array, 0, newArray, 0, _size);
            _array = newArray;
        }
    }
}

