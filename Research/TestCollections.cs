namespace Research;

public class TestCollections
{
    private List<Team> _teams;
    private List<string> _topics;
    private Dictionary<Team, ResearchTeam> _researchTeams;
    private Dictionary<string, ResearchTeam> _researchTeamsByTopic;

    public static ResearchTeam GetResearchTeam(int num) => new ResearchTeam($"Topic{num}", $"Org{num}", num, TimeFrame.TwoYears);

    public TestCollections()
    {
        _teams = new List<Team>();
        _topics = new List<string>();
        _researchTeams = new Dictionary<Team, ResearchTeam>();
        _researchTeamsByTopic = new Dictionary<string, ResearchTeam>();
    }

    public TestCollections(int count) : this()
    {
        for (var i = 0; i < count; i++)
        {
            var researchTeam = GetResearchTeam(i);
            _teams.Add(researchTeam.Team);
            _topics.Add(researchTeam.Topic);
            _researchTeams.Add(researchTeam.Team, researchTeam);
            _researchTeamsByTopic.Add(researchTeam.Topic, researchTeam);
        }
    }

    public int GetTimeElapsedOfSearchInList(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_teams.Contains(researchTeam.Team))
            {
                _teams.Add(researchTeam.Team);
            }
        });
    }

    public int GetTimeElapsedOfSearchInDictionary(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_researchTeams.ContainsKey(researchTeam.Team))
            {
                _researchTeams.Add(researchTeam.Team, researchTeam);
            }
        });
    }

    public int GetTimeElapsedOfSearchInDictionaryByTopic(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_researchTeamsByTopic.ContainsKey(researchTeam.Topic))
            {
                _researchTeamsByTopic.Add(researchTeam.Topic, researchTeam);
            }
        });
    }

    public int GetTimeElapsedOfSearchInListTopic(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_topics.Contains(researchTeam.Topic))
            {
                _topics.Add(researchTeam.Topic);
            }
        });
    }
}