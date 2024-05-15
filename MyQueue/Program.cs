namespace MyQueues
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyQueue<string> st = new MyQueue<string>();
            st.Enqueue("новый элемент");
            st.Enqueue("второй элемент");
            st.Dequeue();
            st.Enqueue("третий элемент");
            st.Enqueue("четвертый элемент");
            st.Enqueue("пятый элемент");
            st.Enqueue("шестой элемент");

            foreach (var item in st)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(st.Contains("элемент"));

            var firstElement = st.Peek();
            Console.WriteLine(firstElement);

            Console.WriteLine(st.Count);

            st.Clear();
            foreach (var item in st)
            {
                Console.WriteLine(item);
            }
        }
    }
}
