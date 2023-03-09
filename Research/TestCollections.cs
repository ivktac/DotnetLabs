namespace Research;

public class TestCollections
{
    private int _count;
    private List<Team> _teams;
    private List<string> _topics;
    private Dictionary<Team, ResearchTeam> _researchTeams;
    private Dictionary<string, ResearchTeam> _researchTeamsByTopic;

    public static ResearchTeam GetResearchTeam(int num) => new ResearchTeam($"Topic{num}", $"Org{num}", num, TimeFrame.TwoYears);

    public TestCollections() : this(0) { }

    public TestCollections(int count)
    {
        _count = count;
        _teams = new List<Team>(count);
        _topics = new List<string>(count);
        _researchTeams = new Dictionary<Team, ResearchTeam>(count);
        _researchTeamsByTopic = new Dictionary<string, ResearchTeam>(count);
    }

    public int Count => _count;

    public ResearchTeam this[int index] => GetResearchTeam(index);

    public int GetTimeElapsedOfSearchInList(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_teams.Contains(researchTeam.Team))
            {
            }
        });
    }

    public int GetTimeElapsedOfSearchInDictionary(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_researchTeams.ContainsKey(researchTeam.Team))
            {
            }
        });
    }

    public int GetTimeElapsedOfSearchInDictionaryByTopic(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_researchTeamsByTopic.ContainsKey(researchTeam.Topic))
            {
            }
        });
    }

    public int GetTimeElapsedOfSearchInListTopic(ResearchTeam researchTeam)
    {
        return ExtensionMethods.GetTimeElapsed(() =>
        {
            if (!_topics.Contains(researchTeam.Topic))
            {
            }
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