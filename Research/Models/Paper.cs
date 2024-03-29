using Research.Interfaces;

namespace Research.Models;

public class Paper : INameAndCopy
{
    private DateTime _publishDate = default!;

    public Paper()
        : this("Untitled", new(), DateTime.Now) { }

    public Paper(string title, Person author, DateTime publishDate)
    {
        Title = title;
        Author = author;
        PublishDate = publishDate;
    }

    public string Title { get; set; }

    public Person Author { get; init; }

    public DateTime PublishDate
    {
        get => _publishDate;
        init
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("Publish date cannot be in the future.");
            }

            _publishDate = value;
        }
    }

    string INameAndCopy.Name
    {
        get => Title;
        set => Title = value;
    }

    public override string ToString() =>
        $"{Title} by {Author.ToShortString()} ({PublishDate.ToShortDateString()})";

    public virtual object DeepCopy() => MemberwiseClone();
}
