using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

/*
============================================================
YIELD RETURN CHEAT SHEET
============================================================

WHAT IS yield return?
---------------------

yield return allows lazy iteration.

Instead of creating the whole collection in memory,
items are generated one-by-one during enumeration.

Mental model:
- List<T>      => download whole movie first
- yield return => Netflix streaming

============================================================
IMPORTANT CONCEPTS
============================================================

- Deferred execution
- Lazy evaluation
- Streaming
- State machine generation
- IEnumerable<T>
- IEnumerator<T>

The compiler secretly transforms iterator methods
into state machine classes.

============================================================
BASIC EXAMPLE
============================================================
*/

namespace DotNetEngineeringLab.CSharp.YieldReturn;

public static class YieldReturnCheatSheet
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
    OUTPUT:

    var items = BasicExample();

    Console.WriteLine("Created");

    foreach (var item in items)
    {
        Console.WriteLine(item);
    }

    RESULT:

    Created
    Method Started
    1
    Middle
    2
    Finished

    Notice:
    The method does NOT execute immediately.
    Execution happens during enumeration.
    */

    // ========================================================
    // INFINITE SEQUENCE
    // ========================================================

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

    foreach (var n in InfiniteCounter().Take(5))
    {
        Console.WriteLine(n);
    }

    RESULT:
    0 1 2 3 4
    */

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

    /*
    BENEFIT:

    No additional list allocation.

    Data streams item-by-item.
    */

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

    /*
    WHY IMPORTANT?

    File.ReadAllLines():
    - loads entire file into memory

    yield return approach:
    - streams line-by-line
    - memory efficient

    Very important for:
    - log processing
    - big files
    - ETL systems
    */

    // ========================================================
    // ASYNC STREAMING
    // ========================================================

    public static async IAsyncEnumerable<string> StreamTokens()
    {
        yield return "Hello";

        await Task.Delay(500);

        yield return "from";

        await Task.Delay(500);

        yield return "yield return";
    }

    /*
    MODERN USAGE:

    Used heavily in:
    - OpenAI streaming
    - AI agents
    - SignalR
    - SSE
    - chat applications

    Example:

    await foreach (var token in StreamTokens())
    {
        Console.Write(token + " ");
    }
    */

    // ========================================================
    // yield break
    // ========================================================

    public static IEnumerable<int> YieldBreakExample()
    {
        yield return 1;

        yield break;

        // Never executes
        yield return 2;
    }

    // ========================================================
    // COMMON PITFALLS
    // ========================================================

    /*
    PITFALL #1
    Multiple Enumeration

    Every enumeration reruns the method.

    --------------------------------------------------------

    PITFALL #2
    Deferred Exceptions

    Exceptions happen DURING enumeration.

    --------------------------------------------------------

    PITFALL #3
    DbContext Lifetime Problem

    DANGEROUS:

    public IEnumerable<User> GetUsers()
    {
        using var db = new AppDbContext();

        foreach(var user in db.Users)
        {
            yield return user;
        }
    }

    The DbContext may be disposed before enumeration completes.

    --------------------------------------------------------

    PITFALL #4
    Hidden Execution Timing

    Method body executes later than expected.
    */

    // ========================================================
    // WHEN TO USE
    // ========================================================

    /*
    USE yield return WHEN:

    ✔ Streaming data
    ✔ Large datasets
    ✔ Pipelines
    ✔ File processing
    ✔ AI token streaming
    ✔ Memory optimization

    AVOID WHEN:

    ✘ Random access needed
    ✘ Multiple enumeration dangerous
    ✘ Complex thread-sensitive logic
    ✘ Immediate materialization preferred
    */
}
