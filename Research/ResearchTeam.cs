using System.Collections;

namespace Research;

public partial class ResearchTeam : Team, INameAndCopy, IEnumerable<Person>
{
    private string _topic = default!;
    private TimeFrame _timeFrame = default!;
    private List<Person> _members = default!;
    private List<Paper> _publications = default!;

    public ResearchTeam(string topic, string organization, int registrationNumber, TimeFrame timeFrame)
        : base(organization, registrationNumber)
    {
        Topic = topic;
        TimeFrame = timeFrame;
        Members = new List<Person>();
        Publications = new List<Paper>();
    }

    public ResearchTeam() : this("No topic", "No organization", 0, TimeFrame.Year) { }

    public bool this[TimeFrame timeFrame] => TimeFrame == timeFrame;

    public string Topic
    {
        get => _topic;
        init => _topic = value;
    }

    public TimeFrame TimeFrame
    {
        get => _timeFrame;
        init => _timeFrame = value;
    }

    public List<Paper> Publications
    {
        get => _publications;
        private set => _publications = value;
    }

    public List<Person> Members
    {
        get => _members;
        private set => _members = value;
    }

    /// <summary>Gets the last publication.</summary>
    /// <returns>The last publication.</returns>
    public Paper? LastPublication
    {
        get
        {
            if (Publications.Count == 0)
            {
                return null;
            }

            Paper lastPublication = Publications[0];
            foreach (Paper publication in Publications)
            {
                if (publication.PublishDate > lastPublication.PublishDate)
                {
                    lastPublication = publication;
                }
            }

            return lastPublication;
        }
    }

    public Team Team
    {
        get => new(Organization, RegistrationNumber);
        set => (Organization, RegistrationNumber) = (value.Organization, value.RegistrationNumber);
    }

    /// <summary>Adds a papers to the publications.</summary>
    /// <param name="papers">The papers to add.</param>
    public void AddPapers(params Paper[]? papers)
    {
        if (papers is null)
        {
            return;
        }

        foreach (Paper paper in papers)
        {
            if (paper is not null)
            {
                Publications.Add(paper);
            }
        }
    }

    /// <summary>Adds a members to the members.</summary>
    /// <param name="members">The members to add.</param>
    public void AddMembers(params Person[]? members)
    {
        if (members is null)
        {
            return;
        }

        foreach (Person member in members)
        {
            if (member is not null)
            {
                Members.Add(member);
            }
        }
    }

    /// <summary>Gets the members of the team who have no publications.</summary>
    /// <returns>The members of the team who have no publications.</returns>
    public IEnumerable<Person> GetPersonsWithNoPublications()
    {
        foreach (var member in this)
        {
            if (Publications.FindAll(publication => publication.Author.Equals(member)).Count == 0)
            {
                yield return member;
            }
        }
    }

    /// <summary>Gets the members of the team who have more than n publications. If n is not specified, returns the members of the team who have at least one publication.</summary>
    /// <param name="n">The number of publications.</param>
    /// <returns>The members of the team who have more than n publications.</returns>
    public IEnumerable<Person> GetPersonWithPublications(int n = 0)
    {
        foreach (var member in this)
        {
            if (Publications.FindAll(publication => publication.Author.Equals(member)).Count > n)
            {
                yield return member;
            }
        }
    }

    /// <summary>
    /// Gets the papers published within the last n years.
    /// </summary>
    /// <param name="n">The number of years.</param>
    /// <returns>The papers published within the last n years.</returns>
    public IEnumerable<Paper> GetPapersWithinLastYears(int n)
    {
        foreach (var publication in Publications)
        {
            if (publication.PublishDate.Year >= DateTime.Now.Year - n)
            {
                yield return publication;
            }
        }
    }

    /// <summary>
    /// Gets the papers published in the last year.
    /// </summary>
    /// <returns>The papers published in the last year.</returns>
    public IEnumerable<Paper> GetPapersInLastYear() => GetPapersWithinLastYears(1);

    public sealed override string ToString()
    {
        string result = $"Topic: {Topic}\nOrganization: " + base.ToString() + $"\nTime frame: {TimeFrame}\nPublications:\n"; ;

        var stringBuilder = new System.Text.StringBuilder(result);
        foreach (Paper publication in Publications)
        {
            stringBuilder.AppendLine(publication.ToString());
        }

        return stringBuilder.ToString();
    }

    public string ToShortString() => $"Topic: {Topic}\nOrganization" + base.ToString() + $"Time frame: {TimeFrame}\n";

    public sealed override object DeepCopy()
    {
        var researchTeam = MemberwiseClone() as ResearchTeam;

        if (researchTeam is null)
        {
            throw new NullReferenceException("ResearchTeam can not be null");
        }

        researchTeam.Members = new List<Person>(Members);
        researchTeam.Publications = new List<Paper>(Publications);
        researchTeam.Team = (Team)Team.DeepCopy();

        return researchTeam;
    }

    public IEnumerator<Person> GetEnumerator() => new ResearchTeamEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}