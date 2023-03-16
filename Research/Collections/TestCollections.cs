using Research.Enums;
using Research.Models;
using Research.Extensions;

namespace Research.Collections;

public class TestCollections
{
    private List<Team> _teams;
    private List<string> _topics;
    private Dictionary<Team, ResearchTeam> _researchTeams;
    private Dictionary<string, ResearchTeam> _researchTeamsByTopic;

    public static ResearchTeam GetResearchTeam(int num) =>
        new ResearchTeam($"Topic{num}", $"Org{num}", num, TimeFrame.TwoYears);

    public TestCollections()
        : this(0) { }

    public TestCollections(int count)
    {
        Count = count;
        _teams = new(count);
        _topics = new(count);
        _researchTeams = new(count);
        _researchTeamsByTopic = new(count);
    }

    public int Count { get; private init; }

    public ResearchTeam this[int index] => GetResearchTeam(index);

    public int GetTimeElapsedOfSearchInList(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!_teams.Contains(researchTeam.Team)) { }
        });
    }

    public int GetTimeElapsedOfSearchInDictionary(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!_researchTeams.ContainsKey(researchTeam.Team)) { }
        });
    }

    public int GetTimeElapsedOfSearchInDictionaryByTopic(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!_researchTeamsByTopic.ContainsKey(researchTeam.Topic)) { }
        });
    }

    public int GetTimeElapsedOfSearchInListTopic(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!_topics.Contains(researchTeam.Topic)) { }
        });
    }

    public void InitializeDefaultValues()
    {
        for (int i = 0; i < Count; i++)
        {
            var researchTeam = GetResearchTeam(i);

            _teams.Add(researchTeam.Team);
            _topics.Add(researchTeam.Topic);
            _researchTeams.Add(researchTeam.Team, researchTeam);
            _researchTeamsByTopic.Add(researchTeam.Topic, researchTeam);
        }
    }
}
