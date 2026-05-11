using System.Collections.Generic;
using System.IO;

namespace YieldReturnLab.FileStreaming;

public static class FileReader
{
    public static IEnumerable<string> ReadLines(string path)
    {
        using var reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine()!;
        }
    }
}
