using System.Collections;

using Research.Enums;
using Research.Models;
using Research.Services;

namespace Research.Collections;

public class ResearchTeamCollection : IEnumerable<ResearchTeam>
{
    public ResearchTeamCollection(): this(new()) { }

    public ResearchTeamCollection(List<ResearchTeam> researchTeams) =>
        ResearchTeams = researchTeams;

    public List<ResearchTeam> ResearchTeams { get; private set; }

    public int MinimumRegistrationNumber
    {
        get
        {
            if (ResearchTeams is null)
            {
                return 0;
            }

            return ResearchTeams.Min(x => x.Team.RegistrationNumber);
        }
    }

    public IEnumerable<ResearchTeam> ResearchTeamWithinTwoYears =>
        ResearchTeams.Where(x => x[TimeFrame.TwoYears]);

    public void AddDefaults() => AddResearchTeams(new ResearchTeam(), new ResearchTeam());

    public void AddResearchTeams(params ResearchTeam[] researchTeams) =>
        ResearchTeams.AddRange(researchTeams);

    public sealed override string? ToString()
    {
        var stringBuilder = new System.Text.StringBuilder();

        foreach (var researchTeam in ResearchTeams)
        {
            stringBuilder.AppendLine(researchTeam.ToString());
        }

        return stringBuilder.ToString();
    }

    public virtual string? ToShortString()
    {
        var stringBuilder = new System.Text.StringBuilder();

        foreach (var researchTeam in ResearchTeams)
        {
            stringBuilder.AppendLine(researchTeam.ToShortString());
        }

        return stringBuilder.ToString();
    }

    public void SortByRegistartionNumber() =>
        ResearchTeams.Sort(
            (x, y) => x.Team.RegistrationNumber.CompareTo(y.Team.RegistrationNumber)
        );

    public void SortByTopic() => ResearchTeams.Sort();

    public void SortByPublicationsCount() =>
        ResearchTeams.Sort(new ResearchPublicationsComparer());

    public List<ResearchTeam> NGroup(int value) =>
        ResearchTeams
            .GroupBy(x => x.Members.Count == value)
            .Aggregate(
                new List<ResearchTeam>(),
                (list, group) =>
                {
                    list.AddRange(group);
                    return list;
                }
            );

    public IEnumerator<ResearchTeam> GetEnumerator() => ResearchTeams.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
