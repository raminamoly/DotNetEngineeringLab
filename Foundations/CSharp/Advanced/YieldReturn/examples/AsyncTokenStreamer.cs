using System.Collections.Generic;
using System.Threading.Tasks;

namespace YieldReturnLab.AsyncStreaming;

public static class TokenStreamer
{
    public static async IAsyncEnumerable<string> StreamTokens()
    {
        yield return "Hello";

        await Task.Delay(500);

        yield return "from";

        await Task.Delay(500);

        yield return "yield return";
    }
}
