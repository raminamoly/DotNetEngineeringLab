using System.Collections.Generic;

namespace YieldReturnLab.Filtering;

public static class CustomFilter
{
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
}
