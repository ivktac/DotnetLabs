using System.Collections;

namespace Research;

class ResearchTeam : Team, INameAndCopy, IEnumerable<Person>
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

    public sealed override object DeepCopy()
    {
        ResearchTeam team = new()
        {
            Topic = Topic,
            Organization = Organization,
            RegistrationNumber = RegistrationNumber,
            TimeFrame = TimeFrame,
            Members = new List<Person>(Members),
            Publications = new List<Paper>(Publications)
        };
        return team;
    }

    public IEnumerator<Person> GetEnumerator() => new ResearchTeamEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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
        init => _publications = value;
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
        init => (Organization, RegistrationNumber) = (value.Organization, value.RegistrationNumber);
    }

    public bool this[TimeFrame timeFrame] => TimeFrame == timeFrame;

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

    public sealed override string ToString()
    {
        string result = $"Topic: {_topic}\nOrganization: " + base.ToString() + $"\nTime frame: {_timeFrame}\nPublications:\n"; ;
        System.Text.StringBuilder sb = new System.Text.StringBuilder(result);
        foreach (Paper publication in _publications)
        {
            sb.AppendLine(publication.ToString());
        }
        return sb.ToString();
    }

    public string ToShortString() => $"Topic: {_topic}\nOrganization" + base.ToString() + $"Time frame: {_timeFrame}\n";

    /// <summary>Gets the members of the team who have no publications.</summary>
    /// <returns>The members of the team who have no publications.</returns>
    public IEnumerable<Person> GetPersonsWithNoPublications() => this.Where(member => Publications.Find(publication => publication.Author.Equals(member)) is null);

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
    public IEnumerable<Paper> GetPapersWithinLastYears(int n) => Publications.Where(publication => publication.PublishDate.Year >= DateTime.Now.Year - n);

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

    public class ResearchTeamEnumerator : IEnumerator<Person>
    {
        private readonly ResearchTeam _researchTeam;

        private int _index = -1;

        public ResearchTeamEnumerator(ResearchTeam researchTeam) => _researchTeam = researchTeam;

        public Person Current => _researchTeam.Members[_index];

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (_index < _researchTeam.Members.Count - 1)
            {
                _index++;
                return true;
            }
            return false;
        }

        public void Reset() => _index = -1;
    }
}