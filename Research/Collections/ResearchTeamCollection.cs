using System.Collections;

using Research.Enums;
using Research.Models;
using Research.Services;

namespace Research.Collections;

public class ResearchTeamCollection : IEnumerable<ResearchTeam>
{
    private List<ResearchTeam> _researchTeams;

    public ResearchTeamCollection(): this(new()) { }

    public ResearchTeamCollection(List<ResearchTeam> researchTeams) =>
        _researchTeams = researchTeams;

    public int MinimumRegistrationNumber
    {
        get
        {
            if (_researchTeams.Count == 0)
            {
                return 0;
            }

            return _researchTeams.Min(x => x.Team.RegistrationNumber);
        }
    }

    public IEnumerable<ResearchTeam> ResearchTeamWithinTwoYears =>
        _researchTeams.Where(x => x[TimeFrame.TwoYears]);

    public void AddDefaults() => AddResearchTeams(new ResearchTeam(), new ResearchTeam());

    public void AddResearchTeams(params ResearchTeam[] researchTeams) =>
        _researchTeams.AddRange(researchTeams);

    public sealed override string? ToString()
    {
        var stringBuilder = new System.Text.StringBuilder();

        foreach (var researchTeam in _researchTeams)
        {
            stringBuilder.AppendLine(researchTeam.ToString());
        }

        return stringBuilder.ToString();
    }

    public virtual string? ToShortString()
    {
        var stringBuilder = new System.Text.StringBuilder();

        foreach (var researchTeam in _researchTeams)
        {
            stringBuilder.AppendLine(researchTeam.ToShortString());
        }

        return stringBuilder.ToString();
    }

    public void SortByRegistartionNumber() =>
        _researchTeams.Sort(
            (x, y) => x.Team.RegistrationNumber.CompareTo(y.Team.RegistrationNumber)
        );

    public void SortByTopic() => _researchTeams.Sort();

    public void SortByPublicationsCount() =>
        _researchTeams.Sort(new ResearchPublicationsComparer());

    public List<ResearchTeam> NGroup(int value) =>
        _researchTeams
            .GroupBy(x => x.Members.Count == value)
            .Aggregate(
                new List<ResearchTeam>(),
                (list, group) =>
                {
                    list.AddRange(group);
                    return list;
                }
            );

    public IEnumerator<ResearchTeam> GetEnumerator() => _researchTeams.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
