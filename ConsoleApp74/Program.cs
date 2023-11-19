namespace ConsoleApp74
{
    class Program
    {
        static Mutex mutex = new Mutex();
        static int[] data = { 1, 2, 3, 4, 5 };
        static int incrementValue = 2;

        static void Main()
        {
            Thread firstThread = new Thread(ModifyArray);
            Thread secondThread = new Thread(FindMax);

            firstThread.Start();
            secondThread.Start();

            firstThread.Join();
            secondThread.Join();
        }

        static void ModifyArray()
        {
            mutex.WaitOne();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] += incrementValue;
                Console.WriteLine($"Thread 1: Modified element at index {i}, new value: {data[i]}");
                Thread.Sleep(100);
            }

            mutex.ReleaseMutex();
        }

        static void FindMax()
        {
            mutex.WaitOne();

            int max = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                    max = data[i];
            }

            Console.WriteLine($"Thread 2: Maximum value in the array: {max}");

            mutex.ReleaseMutex();
        }
    }

}