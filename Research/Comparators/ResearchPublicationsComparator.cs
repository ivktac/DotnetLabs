using Research.Models;

namespace Research.Comparators;

public class ResearchPublicationsComparator : IComparer<ResearchTeam>
{
    public int Compare(ResearchTeam? x, ResearchTeam? y)
    {
        if (x is null && y is null)
        {
            return 0;
        }

        if (x is null)
        {
            return -1;
        }

        if (y is null)
        {
            return 1;
        }

        return x.Publications.Count.CompareTo(y.Publications.Count);
    }
}
