using System.Collections;

namespace Research;

public partial class ResearchTeam : Team, INameAndCopy, IEnumerable<Person>, IComparable<ResearchTeam>
{
    private string _topic;
    private TimeFrame _timeFrame;
    private List<Person> _members;
    private List<Paper> _publications;

    public ResearchTeam(string topic, string organization, int registrationNumber, TimeFrame timeFrame)
        : base(organization, registrationNumber)
    {
        _topic = topic;
        _timeFrame = timeFrame;
        _members = new List<Person>();
        _publications = new List<Paper>();
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
        init
        {
            if (value == null)
            {
                throw new ArgumentNullException("Publications cannot be null");
            }
            _publications = value;
        }
    }

    public List<Person> Members
    {
        get => _members;
        init => _members = value;
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

    /// <summary>Gets the members of the team who have no publications.</summary>
    /// <returns>The members of the team who have no publications.</returns>
    public IEnumerable<Person> GetPersonsWithNoPublications() => this.Where(member => Publications.Find(publication => publication.Author.Equals(member)) is null); // TODO: Do I need LINQ and performance is not important here?

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
    public IEnumerable<Paper> GetPapersWithinLastYears(int n) => Publications.Where(publication => publication.PublishDate.Year >= DateTime.Now.Year - n); // TODO: Do I need LINQ and performance is not important here?

    /// <summary>
    /// Gets the papers published in the last year.
    /// </summary>
    /// <returns>The papers published in the last year.</returns>
    public IEnumerable<Paper> GetPapersInLastYear()
    {
        // TODO: Do I really need this method?
        foreach (var publication in Publications)
        {
            if (publication.PublishDate.Year >= DateTime.Now.Year - 1)
            {
                yield return publication;
            }
        }
    }

    public sealed override string ToString()
    {
        string result = $"Topic: {_topic}\nOrganization: " + base.ToString() + $"\nTime frame: {_timeFrame}\nPublications:\n"; ;

        var stringBuilder = new System.Text.StringBuilder(result);
        foreach (Paper publication in _publications)
        {
            stringBuilder.AppendLine(publication.ToString());
        }

        stringBuilder.AppendLine("\nMembers:\n");
        foreach (Person member in _members)
        {
            stringBuilder.AppendLine(member.ToString());
        }

        return stringBuilder.ToString();
    }

    public string ToShortString() => $"Topic: {_topic}\nOrganization" + base.ToString() + $"Time frame: {_timeFrame}\nPublications: {Publications.Count}\nMembers: {Members.Count}";

    public sealed override object DeepCopy()
    {
        var researchTeam = MemberwiseClone() as ResearchTeam;

        if (researchTeam is null)
        {
            throw new NullReferenceException("ResearchTeam can not be null");
        }

        researchTeam._topic = Topic;
        researchTeam._timeFrame = TimeFrame;
        researchTeam._members = new List<Person>(Members);
        researchTeam._publications = new List<Paper>(Publications);
        researchTeam.Team = (Team)Team.DeepCopy();

        return researchTeam;
    }

    public IEnumerator<Person> GetEnumerator() => new ResearchTeamEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int CompareTo(ResearchTeam? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Topic.CompareTo(other.Topic);
    }
}