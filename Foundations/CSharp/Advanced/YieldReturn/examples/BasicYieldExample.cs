using System;
using System.Collections.Generic;

namespace YieldReturnLab.Basic;

public static class BasicYieldExample
{
    public static IEnumerable<int> GetNumbers()
    {
        Console.WriteLine("Method Started");

        yield return 1;

        Console.WriteLine("Middle");

        yield return 2;

        Console.WriteLine("Finished");
    }

    public static void Run()
    {
        var numbers = GetNumbers();

        Console.WriteLine("Enumeration Started");

        foreach (var number in numbers)
        {
            Console.WriteLine($"Value: {number}");
        }
    }
}
