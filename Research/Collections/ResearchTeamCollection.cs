using System.Collections;
using Reseach.Services;
using Research.Enums;
using Research.Models;
using Research.Services;

namespace Research.Collections;

public class ResearchTeamCollection : IEnumerable<ResearchTeam>
{
    public ResearchTeamCollection(): this(new("No name collection")) { }

    public ResearchTeamCollection(string name) : this(name, new()) { }

    public ResearchTeamCollection(string name, List<ResearchTeam> researchTeams)
    {
        Name = name;
        ResearchTeams = researchTeams;
    }

    public List<ResearchTeam> ResearchTeams { get; private set; }

    public string Name { get; set; }

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

    public ResearchTeam this[int index]
    {
        get => ResearchTeams[index];
        init => ResearchTeams[index] = value;
    }

    public delegate void TeamListHandler(object source, TeamListHandlerEventArgs args);

    public event TeamListHandler ResearchTeamAdded = default!;

    public event TeamListHandler ResearchTeamInserted = default!;

    public void AddDefaults() => AddResearchTeams(new ResearchTeam(), new ResearchTeam());

    public void AddResearchTeams(params ResearchTeam[] researchTeams)
    {
        foreach (var researchTeam in researchTeams)
        {
            ResearchTeams.Add(researchTeam);
            OnResearchTeamAdded();
        }
    }

    public bool Remove(int index)
    {
        if (index >= 0 && index < ResearchTeams.Count)
        {
            ResearchTeams.RemoveAt(index);
            return true;
        }

        return false;
    }

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

    public void InsertAt(int index, ResearchTeam researchTeam)
    {
        if (index >= 0 && index < ResearchTeams.Count)
        {
            ResearchTeams.Insert(index, researchTeam);
            OnResearchTeamInserted(index);
            return;
        }

        ResearchTeams.Add(researchTeam);
        OnResearchTeamAdded();
    }

    public IEnumerator<ResearchTeam> GetEnumerator() => ResearchTeams.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    protected virtual void OnResearchTeamAdded() =>
        ResearchTeamAdded?.Invoke(
            this,
            new TeamListHandlerEventArgs(
                Name,
                "Added element to collection",
                ResearchTeams.Count - 1
            )
        );

    protected virtual void OnResearchTeamInserted(int index) =>
        ResearchTeamInserted?.Invoke(
            this,
            new TeamListHandlerEventArgs(Name, "Inserted element to collection", index)
        );
}
