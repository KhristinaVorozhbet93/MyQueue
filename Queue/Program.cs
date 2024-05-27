namespace Queues
{
    internal class Program
    {
        static void Main(string[] args)
        {                 
            MyQueue<string> st = new MyQueue<string>();

            var task = Task.Run(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    st.Enqueue(i.ToString());
                }
            });
            for (int i = 0; i < 10000000; i++)
            {
                st.Enqueue(i.ToString());
            }
            task.Wait();
            Console.WriteLine(st.Count);
        }
    }
}
