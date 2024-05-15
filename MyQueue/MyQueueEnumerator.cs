using System.Collections;

namespace MyQueues
{
    public class MyQueueEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] _items;
        int position = -1;
        public MyQueueEnumerator(T[] collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            _items = collection;
        }
        public T Current
        {
            get
            {
                if (position >= _items.Length || position == -1)
                {
                    throw new ArgumentException();
                }
                return _items[position];
            }
        }
        object IEnumerator.Current => Current!;

        public bool MoveNext()
        {
            position++;
            while (position < _items.Length && _items[position] == null)
            {
                position++;
            }
            return position < _items.Length;
        }

        public void Dispose() { }
        public void Reset() => position = -1;
    }
}



