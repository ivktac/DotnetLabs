namespace Research;

public class Paper
{
    public Paper() : this("Untitled", new Person(), DateTime.Now) { }

    public Paper(string title, Person author, DateTime publishDate)
    {
        Title = title;
        Author = author;
        PublishDate = publishDate;
    }
    
    public string Title { get; set; }

    public Person Author { get; init; }

    public DateTime PublishDate { get; init; }

    public override string ToString() => $"{Title} by {Author.ToShortString()} ({PublishDate.ToShortDateString()})";
}