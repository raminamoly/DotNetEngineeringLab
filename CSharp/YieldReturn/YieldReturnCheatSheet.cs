using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

/*
============================================================
YIELD RETURN CHEAT SHEET
============================================================

Structure Philosophy:

CSharp/
 └── YieldReturn/
      └── YieldReturnCheatSheet.cs

One concept = one folder
One fast-review file per concept
Theory + examples together
Optimized for feature recall

============================================================
WHAT IS yield return?
============================================================

yield return enables lazy iteration.

Instead of building the whole collection in memory,
items are generated one-by-one during enumeration.

Mental Model:
- List<T>      => Download whole movie first
- yield return => Netflix streaming

============================================================
CORE CONCEPTS
============================================================

- Deferred Execution
- Lazy Evaluation
- Streaming
- State Machines
- IEnumerable<T>
- IEnumerator<T>

Compiler secretly generates a hidden iterator state machine.

============================================================
BASIC EXAMPLE
============================================================
*/

namespace DotNetEngineeringLab.CSharp.YieldReturn;

public static class YieldReturnCheatSheet
{
    public static IEnumerable<int> BasicExample()
    {
        Console.WriteLine("Started");

        yield return 1;

        Console.WriteLine("Middle");

        yield return 2;

        Console.WriteLine("Finished");
    }

    /*
    Example:

    var items = BasicExample();

    foreach(var item in items)
    {
        Console.WriteLine(item);
    }

    Output:

    Started
    1
    Middle
    2
    Finished

    Important:
    Method executes during enumeration.
    */

    // ========================================================
    // INFINITE GENERATOR
    // ========================================================

    public static IEnumerable<int> InfiniteCounter()
    {
        int i = 0;

        while (true)
        {
            yield return i++;
        }
    }

    // ========================================================
    // CUSTOM FILTERING
    // ========================================================

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

    // ========================================================
    // FILE STREAMING
    // ========================================================

    public static IEnumerable<string> ReadLines(string path)
    {
        using var reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine()!;
        }
    }

    // ========================================================
    // ASYNC STREAMING
    // ========================================================

    public static async IAsyncEnumerable<string> StreamTokens()
    {
        yield return "Hello";

        await Task.Delay(300);

        yield return "from";

        await Task.Delay(300);

        yield return "yield return";
    }

    // ========================================================
    // yield break
    // ========================================================

    public static IEnumerable<int> YieldBreakExample()
    {
        yield return 1;

        yield break;

        yield return 2;
    }

    // ========================================================
    // COMMON PITFALLS
    // ========================================================

    /*
    1. Multiple Enumeration
       Method reruns every enumeration.

    2. Deferred Exceptions
       Exception occurs during enumeration.

    3. DbContext Lifetime Problems

       Dangerous:

       using var db = new AppDbContext();

       foreach(var x in db.Users)
       {
           yield return x;
       }

    4. Hidden Execution Timing
       Method body executes later.
    */

    // ========================================================
    // WHEN TO USE
    // ========================================================

    /*
    GOOD FOR:

    ✔ Streaming
    ✔ Large datasets
    ✔ AI token streaming
    ✔ File processing
    ✔ Pipelines
    ✔ Memory optimization

    AVOID WHEN:

    ✘ Random access needed
    ✘ Immediate materialization preferred
    ✘ Complex thread-sensitive workflows
    */
}
