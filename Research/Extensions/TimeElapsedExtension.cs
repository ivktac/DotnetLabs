using System;

namespace Research.Extensions;

internal static class TimeElapsedExtension
{
    internal static int GetTimeElapsed(this Action action)
    {
        var startTime = Environment.TickCount;

        action();

        return Environment.TickCount - startTime;
    }
}
