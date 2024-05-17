namespace Queues
{
    internal class Program
    {
        static void Main(string[] args)
        {       
            MyQueue<string> st = new MyQueue<string>();
            st.Enqueue("новый элемент");
            st.Enqueue("второй элемент");      
            st.Enqueue("третий элемент");
            st.Dequeue();

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
