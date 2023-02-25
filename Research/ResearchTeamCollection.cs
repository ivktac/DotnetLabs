namespace Research;

public class ResearchTeamCollection
{
    private List<ResearchTeam> _researchTeams;

    public ResearchTeamCollection() : this(new List<ResearchTeam>()) { }

    public ResearchTeamCollection(List<ResearchTeam> researchTeams) => _researchTeams = researchTeams;

    public void AddDefaults() => AddResearchTeams(new ResearchTeam(), new ResearchTeam());

    public void AddResearchTeams(params ResearchTeam[] researchTeams) => _researchTeams.AddRange(researchTeams);

    public sealed override string? ToString()
    {
        var stringBuilder = new System.Text.StringBuilder();

        return stringBuilder.ToString();
    }

    public virtual string? ToShortString()
    {
        var stringBuidler = new System.Text.StringBuilder();

        return stringBuidler.ToString();
    }
}