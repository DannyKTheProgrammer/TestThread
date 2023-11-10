using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("Monitoring processes...");

        // Получаем текущий процесс
        var process = Process.GetCurrentProcess();

        // Запоминаем начальное процессорное время
        var startTime = process.TotalProcessorTime;

        // Выводим информацию о процессе каждую секунду в течение 10 секунд
        for (int i = 0; i < 10; i++)
        {
            // Получаем текущее процессорное время
            var currentTime = process.TotalProcessorTime;

            // Вычисляем процессорное время, использованное за последнюю секунду.
            // У меня почти всегда 0-1%, лучше смотреть ещё на другие средства мониторинга.
            var cpuUsage = (currentTime - startTime).TotalSeconds / Environment.ProcessorCount * 100.0;

            Console.WriteLine($"CPU Usage: {cpuUsage}% on Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            // Вызываем метод с блокировкой потока
            MethodWithThreadSleep();
            // Вызываем метод без блокировки потока
            MethodWithTaskDelay();
            // Обновляем начальное процессорное время
            startTime = currentTime;
        }

        Console.WriteLine("Monitoring complete.");
    }

    static void MethodWithThreadSleep()
    {
        Console.WriteLine($"MethodWithThreadSleep started on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(5000);
        Console.WriteLine($"MethodWithThreadSleep completed on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
    }

    static async void MethodWithTaskDelay()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"MethodWithTaskDelay started on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(5000);
        Console.WriteLine($"MethodWithTaskDelay completed on Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
