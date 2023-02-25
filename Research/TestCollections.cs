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

    public int GetTimeElapsedOfSearch(Action action)
    {
        var startTime = Environment.TickCount;

        action();

        return Environment.TickCount - startTime;
    }
}