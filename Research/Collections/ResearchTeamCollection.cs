using System.Collections;
using Reseach.Services;
using Research.Enums;
using Research.Models;
using Research.Services;

namespace Research.Collections;

public class ResearchTeamCollection : IEnumerable<ResearchTeam>
{
    private List<ResearchTeam> _researchTeams;

    public ResearchTeamCollection()
        : this("ResearchTeamCollection") { }

    public ResearchTeamCollection(string name) : this(name, new()) { }

    public ResearchTeamCollection(string name, List<ResearchTeam> researchTeams)
    {
        Name = name;
        _researchTeams = researchTeams;
    }

    public string Name { get; set; }

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

    public ResearchTeam this[int index]
    {
        get => _researchTeams[index];
        init => _researchTeams[index] = value;
    }

    public delegate void TeamListHandler(object source, TeamListHandlerEventArgs args);

    public event TeamListHandler ResearchTeamAdded = default!;

    public event TeamListHandler ResearchTeamInserted = default!;

    public void AddDefaults() => AddResearchTeams(new ResearchTeam(), new ResearchTeam());

    public void AddResearchTeams(params ResearchTeam[] researchTeams)
    {
        foreach (var researchTeam in researchTeams)
        {
            _researchTeams.Add(researchTeam);
            OnResearchTeamAdded();
        }
    }

    public bool Remove(int index)
    {
        if (index >= 0 && index < _researchTeams.Count)
        {
            _researchTeams.RemoveAt(index);
            return true;
        }

        return false;
    }

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

    public void InsertAt(int index, ResearchTeam researchTeam)
    {
        if (index >= 0 && index < _researchTeams.Count)
        {
            _researchTeams.Insert(index, researchTeam);
            OnResearchTeamInserted(index);
            return;
        }

        _researchTeams.Add(researchTeam);
        OnResearchTeamAdded();
    }

    public IEnumerator<ResearchTeam> GetEnumerator() => _researchTeams.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    protected virtual void OnResearchTeamAdded() =>
        ResearchTeamAdded?.Invoke(
            this,
            new TeamListHandlerEventArgs(
                Name,
                "Added element to collection",
                _researchTeams.Count - 1
            )
        );

    protected virtual void OnResearchTeamInserted(int index) =>
        ResearchTeamInserted?.Invoke(
            this,
            new TeamListHandlerEventArgs(Name, "Inserted element to collection", index)
        );
}
