using System.Collections;

namespace Research;

class ResearchTeam : Team, INameAndCopy
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

    //
    // Summary:
    //      Gets the last publication of the team.
    // 
    // Returns:
    //      The last publication of the team.
    //
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

    public IEnumerable<Person> GetPersonsWithNoPublications()
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Paper> GetPapersWithinLastYears(int n)
    {
        throw new NotImplementedException();
    }
}