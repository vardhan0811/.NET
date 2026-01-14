
namespace Day19Threading
{
    public class Day19Threading
    {
        public static void Run(string[] args)
        {
            Thread t1 = new Thread(Task1);
            Thread t2 = new Thread(Task2);
            t1.Start();
            // t1.Join();
            t2.Start();
            Console.WriteLine("Main Thread Completed");
            CallMethod();
        }

        public static async Task AsyncMethod()
        {
            Console.WriteLine("Task Started");
            await Task.Delay(2000); // Simulate async work
            Console.WriteLine("Task Completed");
        }

        public static async void CallMethod()
        {
            string result = await FetchDataAsync("https://jsonplaceholder.typicode.com/todos");
            Console.WriteLine(result);
            await AsyncMethod();
        }

        public static async Task<string> FetchDataAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }

        public static void Task1()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Task 1 - Count {i}");
                Thread.Sleep(500); // Simulate work
            }
        }

        public static void Task2()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Task 2 - Count {i}");
                Thread.Sleep(500); // Simulate work
            }
        }
    }
}
