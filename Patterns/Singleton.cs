using System;

namespace singleton;

internal class SingletonPattern()
{
    public static void SingletonModel()
    {
        // Create multiple tasks to call the Singleton instance
        Task[] tasks = new Task[10];

        for (int i = 0; i < 10; i++)
        {
            int taskNumber = i;
            tasks[i] = Task.Run(() =>
            {
                Singleton singleton = Singleton.Instance;
                singleton.DoSomething(taskNumber);
            });
        }

        // Wait for all tasks to complete
        Task.WaitAll(tasks);
    }
}

public sealed class Singleton
{
    private static Singleton _instance = null;

    // Lock object for threading
    private static readonly object _lock = new object();

    // Private constructor to prevent instantiation
    private Singleton()
    {
    }

    // Public static method to provide access to the instance
    public static Singleton Instance
    {
        get
        {
            // Double-check locking mechanism for thread safety
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
            }
            return _instance;
        }
    }

    public void DoSomething(int callNumber)
    {
        Console.WriteLine($"Singleton instance called: {callNumber}. Singleton instance {_instance.GetHashCode()}");
    }
}