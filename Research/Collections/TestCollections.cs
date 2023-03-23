using Research.Enums;
using Research.Models;
using Research.Extensions;

namespace Research.Collections;

public class TestCollections
{
    public static ResearchTeam GetResearchTeam(int num) =>
        new ResearchTeam($"Topic{num}", $"Org{num}", num + 1, TimeFrame.TwoYears);

    public TestCollections()
        : this(0) { }

    public TestCollections(int count)
    {
        Count = count;
        Teams = new(count);
        Topics = new(count);
        ResearchTeams = new(count);
        ResearchTeamsByTopic = new(count);
    }

    public int Count { get; private init; }
    public List<Team> Teams { get; private set; }
    public List<string> Topics { get; private set; }
    public Dictionary<Team, ResearchTeam> ResearchTeams { get; private set; }
    public Dictionary<string, ResearchTeam> ResearchTeamsByTopic { get; private set; }

    public ResearchTeam this[int index] => GetResearchTeam(index);

    public int GetTimeElapsedOfSearchInList(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!Teams.Contains(researchTeam.Team)) { }
        });
    }

    public int GetTimeElapsedOfSearchInDictionary(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!ResearchTeams.ContainsKey(researchTeam.Team)) { }
        });
    }

    public int GetTimeElapsedOfSearchInDictionaryByTopic(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!ResearchTeamsByTopic.ContainsKey(researchTeam.Topic)) { }
        });
    }

    public int GetTimeElapsedOfSearchInListTopic(ResearchTeam researchTeam)
    {
        return TimeElapsedExtension.GetTimeElapsed(() =>
        {
            if (!Topics.Contains(researchTeam.Topic)) { }
        });
    }

    public void InitializeDefaultValues()
    {
        for (int i = 0; i < Count; i++)
        {
            var researchTeam = GetResearchTeam(i);

            Teams.Add(researchTeam.Team);
            Topics.Add(researchTeam.Topic);
            ResearchTeams.Add(researchTeam.Team, researchTeam);
            ResearchTeamsByTopic.Add(researchTeam.Topic, researchTeam);
        }
    }
}
