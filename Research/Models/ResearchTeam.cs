using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

using Research.Interfaces;
using Research.Enumerators;
using Research.Enums;
using Research.Extensions;

namespace Research.Models;

public class ResearchTeam : Team, INameAndCopy, IEnumerable<Person>, IComparable<ResearchTeam>
{
    private string _topic = default!;
    private TimeFrame _timeFrame = default!;
    private List<Person> _members = default!;
    private List<Paper> _publications = default!;
    private ConsoleExtension _console = new();

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
        private set => _topic = value;
    }

    public TimeFrame TimeFrame
    {
        get => _timeFrame;
        private set => _timeFrame = value;
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

    public bool Save(string filename)
    {
        try
        {
            string json = JsonSerializer.Serialize(this);

            File.WriteAllText(filename, json);

            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error while saving to file: {exception.Message}");
            return false;
        }
    }

    public bool Load(string filename)
    {
        try
        {
            string json = File.ReadAllText(filename);

            ResearchTeam researchTeam = JsonSerializer.Deserialize<ResearchTeam>(json)!;

            (Topic, TimeFrame, Members, Publications, Team) = (
                researchTeam.Topic,
                researchTeam.TimeFrame,
                researchTeam.Members,
                researchTeam.Publications,
                researchTeam.Team
            );

            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error while loading from file: {exception.Message}");
            return false;
        }
    }

    public bool AddFromConsole()
    {
        try
        {
            Paper paper = _console.ReadPaper();

            _members.Add(paper.Author);
            _publications.Add(paper);

            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error while adding from console: {exception.Message}");
            return false;
        }
    }

    public string ToShortString() =>
        $"Topic: {Topic}\nOrganization" + base.ToString() + $"Time frame: {TimeFrame}\n";

    public sealed override object DeepCopy()
    {
        var options = new JsonSerializerOptions
        {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };

        var bytes = JsonSerializer.SerializeToUtf8Bytes(this, options);

        return JsonSerializer.Deserialize<ResearchTeam>(bytes, options)!;
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
