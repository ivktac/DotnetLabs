using System.Collections;

using Research.Services;
using Research.Enums;

namespace Research.Models;

public class ResearchTeam : Team, INameAndCopy, IEnumerable<Person>, IComparable<ResearchTeam>
{
    private string _topic = default!;
    private TimeFrame _timeFrame = default!;
    private List<Person> _members = default!;
    private List<Paper> _publications = default!;

    public ResearchTeam()
        : this("No topic", "No organization", 1, TimeFrame.Year) { }

    public ResearchTeam(
        string topic,
        string organization,
        int registrationNumber,
        TimeFrame timeFrame
    )
        : base(organization, registrationNumber)
    {
        Topic = topic;
        TimeFrame = timeFrame;
        Members = new();
        Publications = new();
    }

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

    public void AddPapers(params Paper[]? papers) => Add(papers);

    public void AddMembers(params Person[]? members) => Add(members);

    public IEnumerable<Person> GetPersonsWithNoPublications()
    {
        foreach (var member in this)
        {
            if (Publications.Where(publication => publication.Author.Equals(member)).Count() == 0)
            {
                yield return member;
            }
        }
    }

    public IEnumerable<Person> GetPersonWithPublications(int n = 0)
    {
        foreach (var member in this)
        {
            if (Publications.Where(publication => publication.Author.Equals(member)).Count() > n)
            {
                yield return member;
            }
        }
    }

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

    public IEnumerable<Paper> GetPapersInLastYear() => GetPapersWithinLastYears(1);

    public sealed override string ToString()
    {
        string result =
            $"Topic: {Topic}\nOrganization: "
            + base.ToString()
            + $"\nTime frame: {TimeFrame}\nPublications:\n";
        ;

        var stringBuilder = new System.Text.StringBuilder(result);
        foreach (Paper publication in Publications)
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

    public string ToShortString() =>
        $"Topic: {Topic}\nOrganization" + base.ToString() + $"Time frame: {TimeFrame}\n";

    public sealed override object DeepCopy()
    {
        var researchTeam = (ResearchTeam)MemberwiseClone();

        researchTeam.Members = new List<Person>(Members);
        researchTeam.Publications = new List<Paper>(Publications);
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

    /// <summary>Adds a items to the members or publications.</summary>
    /// <param name="items">The items to add.</param>
    private void Add<T>(params T[]? items)
        where T : class
    {
        if (items is null)
        {
            return;
        }

        foreach (T item in items)
        {
            switch (item)
            {
                case Person person:
                    Members.Add(person);
                    break;
                case Paper paper:
                    Publications.Add(paper);
                    break;
            }
        }
    }
}
