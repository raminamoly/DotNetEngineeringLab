using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

/*
╔══════════════════════════════════════════════════════════════╗
║                     ASYNC / AWAIT                           ║
╚══════════════════════════════════════════════════════════════╝

TECHNOLOGY FOLDER
-----------------
CSharp/

CONCEPT FOLDER
--------------
AsyncAwait/

CHEAT SHEET FILE
----------------
CheatSheet.cs

PURPOSE
-------
Fast engineering recall.
One concept -> one folder -> one decorated cheat sheet.

==============================================================
WHAT IS async/await?
==============================================================

async/await allows NON-BLOCKING asynchronous programming.

The thread is NOT blocked while waiting for:

✔ Database calls
✔ HTTP requests
✔ File IO
✔ External APIs
✔ AI model responses

==============================================================
MENTAL MODEL
==============================================================

Synchronous:
-------------
Wait here until work finishes.

Asynchronous:
-------------
Start work -> return thread -> resume later.

==============================================================
IMPORTANT CONCEPTS
==============================================================

✔ Task
✔ Task<T>
✔ async
✔ await
✔ Non-blocking IO
✔ Thread pool
✔ Synchronization context
✔ Continuations

==============================================================
BASIC EXAMPLE
==============================================================
*/

namespace DotNetEngineeringLab.CSharp.AsyncAwait;

public static class CheatSheet
{
    public static async Task BasicExample()
    {
        Console.WriteLine("Start");

        await Task.Delay(1000);

        Console.WriteLine("Finished After 1 Second");
    }

    /*
    IMPORTANT:

    await DOES NOT block the thread.

    The method pauses and resumes later.
    */

    // ==========================================================
    // RETURNING VALUES
    // ==========================================================

    public static async Task<int> GetNumberAsync()
    {
        await Task.Delay(500);

        return 42;
    }

    // ==========================================================
    // HTTP REQUESTS
    // ==========================================================

    public static async Task<string> DownloadAsync()
    {
        using var client = new HttpClient();

        return await client.GetStringAsync("https://example.com");
    }

    /*
    WHY ASYNC?

    Without async:
    ❌ Thread blocked during network wait

    With async:
    ✔ Thread returned to thread pool
    ✔ Better scalability
    */

    // ==========================================================
    // PARALLEL EXECUTION
    // ==========================================================

    public static async Task ParallelExample()
    {
        var task1 = Task.Delay(1000);
        var task2 = Task.Delay(1000);
        var task3 = Task.Delay(1000);

        await Task.WhenAll(task1, task2, task3);

        Console.WriteLine("All Tasks Finished");
    }

    /*
    BENEFIT:

    Total time ≈ 1 second
    NOT 3 seconds.
    */

    // ==========================================================
    // CANCELLATION TOKEN
    // ==========================================================

    public static async Task CancellationExample(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 10; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine($"Working {i}");

            await Task.Delay(500, cancellationToken);
        }
    }

    // ==========================================================
    // ASYNC STREAMS
    // ==========================================================

    public static async IAsyncEnumerable<int> StreamNumbers()
    {
        for (int i = 0; i < 5; i++)
        {
            await Task.Delay(300);

            yield return i;
        }
    }

    /*
    MODERN USAGE:

    ✔ AI token streaming
    ✔ Chat systems
    ✔ SignalR
    ✔ Real-time dashboards
    */

    // ==========================================================
    // COMMON PITFALLS
    // ==========================================================

    /*
    ❌ .Result
    ❌ .Wait()

    Can cause deadlocks.

    ----------------------------------------------------------

    ❌ async void

    Avoid except event handlers.

    ----------------------------------------------------------

    ❌ Fire-and-forget tasks

    Unobserved exceptions possible.

    ----------------------------------------------------------

    ❌ Blocking async code

    BAD:

    Thread.Sleep(5000);

    GOOD:

    await Task.Delay(5000);
    */

    // ==========================================================
    // WHEN TO USE
    // ==========================================================

    /*
    USE async/await FOR:

    ✔ IO-bound work
    ✔ Database calls
    ✔ APIs
    ✔ File access
    ✔ AI requests
    ✔ Network operations

    AVOID FOR:

    ✘ Pure CPU-heavy loops
    ✘ Tiny synchronous operations

    CPU-heavy work:
    Use Parallelism / Background Workers.
    */
}
