using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

/*
╔══════════════════════════════════════════════════════════════╗
║                     YIELD RETURN                            ║
╚══════════════════════════════════════════════════════════════╝

TECHNOLOGY FOLDER
-----------------
CSharp/

CONCEPT FOLDER
--------------
YieldReturn/

CHEAT SHEET FILE
----------------
CheatSheet.cs

PURPOSE
-------
Fast engineering recall.
One concept -> one folder -> one decorated cheat sheet.

==============================================================
WHAT IS yield return?
==============================================================

yield return enables LAZY ITERATION.

Instead of creating the whole collection immediately,
items are produced one-by-one during enumeration.

MENTAL MODEL
-------------
List<T>      => Download entire movie first
yield return => Netflix streaming

==============================================================
IMPORTANT CONCEPTS
==============================================================

✔ Deferred Execution
✔ Lazy Evaluation
✔ Streaming
✔ IEnumerable<T>
✔ IEnumerator<T>
✔ Compiler-generated state machine

The compiler secretly transforms iterator methods into
state-machine classes.

==============================================================
BASIC EXAMPLE
==============================================================
*/

namespace DotNetEngineeringLab.CSharp.YieldReturn;

public static class CheatSheet
{
    public static IEnumerable<int> BasicExample()
    {
        Console.WriteLine("Method Started");

        yield return 1;

        Console.WriteLine("Middle");

        yield return 2;

        Console.WriteLine("Finished");
    }

    /*
    EXECUTION:

    var items = BasicExample();

    foreach(var item in items)
    {
        Console.WriteLine(item);
    }

    OUTPUT:

    Method Started
    1
    Middle
    2
    Finished

    IMPORTANT:
    Method executes DURING enumeration.
    */

    // ==========================================================
    // INFINITE GENERATOR
    // ==========================================================

    public static IEnumerable<int> InfiniteCounter()
    {
        int i = 0;

        while (true)
        {
            yield return i++;
        }
    }

    /*
    Example:

    foreach(var n in InfiniteCounter().Take(5))
    {
        Console.WriteLine(n);
    }

    RESULT:
    0 1 2 3 4
    */

    // ==========================================================
    // CUSTOM FILTERING
    // ==========================================================

    public static IEnumerable<int> EvenNumbers(IEnumerable<int> numbers)
    {
        foreach (var number in numbers)
        {
            if (number % 2 == 0)
            {
                yield return number;
            }
        }
    }

    // ==========================================================
    // FILE STREAMING
    // ==========================================================

    public static IEnumerable<string> ReadLines(string path)
    {
        using var reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine()!;
        }
    }

    /*
    WHY IMPORTANT?

    File.ReadAllLines() => loads entire file into memory

    yield return approach:
    ✔ line-by-line streaming
    ✔ memory efficient
    ✔ scalable
    */

    // ==========================================================
    // ASYNC STREAMING
    // ==========================================================

    public static async IAsyncEnumerable<string> StreamTokens()
    {
        yield return "Hello";

        await Task.Delay(300);

        yield return "from";

        await Task.Delay(300);

        yield return "yield return";
    }

    /*
    Used heavily in:

    ✔ OpenAI streaming
    ✔ AI agents
    ✔ SignalR
    ✔ Chat systems
    ✔ SSE
    */

    // ==========================================================
    // yield break
    // ==========================================================

    public static IEnumerable<int> YieldBreakExample()
    {
        yield return 1;

        yield break;

        yield return 2;
    }

    // ==========================================================
    // COMMON PITFALLS
    // ==========================================================

    /*
    ❌ Multiple Enumeration
       Method reruns every enumeration.

    ❌ Deferred Exceptions
       Exceptions occur during enumeration.

    ❌ DbContext Lifetime Issues

       Dangerous:

       using var db = new AppDbContext();

       foreach(var user in db.Users)
       {
           yield return user;
       }

    ❌ Hidden Execution Timing
       Method body executes later.
    */

    // ==========================================================
    // WHEN TO USE
    // ==========================================================

    /*
    GOOD FOR:

    ✔ Streaming data
    ✔ Large datasets
    ✔ AI token streaming
    ✔ File processing
    ✔ Pipelines
    ✔ Memory optimization

    AVOID WHEN:

    ✘ Random access needed
    ✘ Immediate materialization preferred
    ✘ Complex thread-sensitive logic
    */
}
