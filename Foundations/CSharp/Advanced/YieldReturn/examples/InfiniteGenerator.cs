using System.Collections.Generic;

namespace YieldReturnLab.Infinite;

public static class InfiniteGenerator
{
    public static IEnumerable<int> Generate()
    {
        int i = 0;

        while (true)
        {
            yield return i++;
        }
    }
}
