using Research.Collections;

namespace Research.Extensions;

public static class TimeElapsedExtension
{
    public static int GetTimeElapsed(this Action action)
    {
        var startTime = Environment.TickCount;

        action();

        return Environment.TickCount - startTime;
    }

    /// <summary>
    /// Compares the elapsed time of a search using a binary search tree and a
    /// sequential search on a collection of items. The search is performed on
    /// the item at the specified index.
    /// </summary>

    public static void CompareElapsedTimeOfSearch(TestCollections collection, int index)
    {
        var researchTeam = collection[index];

        int[] time = new int[4];

        time[0] = collection.GetElapsedTimeOfSearchInTeams(researchTeam);
        time[1] = collection.GetElapsedTimeOfSearchInTopics(researchTeam);
        time[2] = collection.GetElapsedTimeOfSearchInResearchTeams(researchTeam);
        time[3] = collection.GetElapsedTimeOfSearchInResearchTeamsByTopic(researchTeam);

        Console.WriteLine($"Search {index} element in collection with {collection.Count} elements");
        Console.WriteLine($"Time elapsed of search in teams: {time[0]}ms");
        Console.WriteLine($"Time elapsed of search in topics: {time[1]}ms");
        Console.WriteLine($"Time elapsed of search in research teams: {time[2]}ms");
        Console.WriteLine(
            $"Time elapsed of search in dictionary by research teams by topic: {time[3]}ms"
        );
    }
}
