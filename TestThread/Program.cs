using System.Diagnostics;

internal class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid arguments");
            return;
        }

        // create processes 
        if (args.Length == 1)
        {
            const int processesPerCore = 100;
            var totalProcesses = Environment.ProcessorCount * processesPerCore;
            Console.WriteLine($"total processes: {totalProcesses}, method: {args[0]}, runtime: {Environment.Version}");

            ThreadPool.SetMinThreads(totalProcesses, 100);

            var executable = Process.GetCurrentProcess().MainModule.FileName;

            Parallel.For(0, totalProcesses, _ => Process.Start(executable, $"{args[0]} run"));
            return;
        }

        if (args[0] == "TaskDelayLoop")
        {
            // consumes lot of CPU
            await TaskDelayLoop();
        }
        else if (args[0] == "ThreadSleepLoop")
        {
            // consumes negligible amount of CPU
            ThreadSleepLoop();
        }
        else Console.WriteLine("Invalid arguments");

        Console.ReadLine();
    }

    static async Task TaskDelayLoop()
    {
        Console.Write(".");
        while (true)
        {
            await Task.Delay(100);  // consumes lot of CPU
        }
    }

    static void ThreadSleepLoop()
    {
        Console.Write(".");
        while (true)
        {
            Thread.Sleep(100);  // consumes negligible amount of CPU
        }
    }
}