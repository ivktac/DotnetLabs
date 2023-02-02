namespace Research;

class Paper
{
    public string Title { get; set; }
    public Person Author { get; init; } = default!;
    public DateTime PublishDate { get; init; } = default!;

    public Paper(string title, Person author, DateTime publishDate)
    {
        Title = title;
        Author = author;
        PublishDate = publishDate;
    }

    public Paper() : this("Untitled", new Person(), DateTime.Now) { }

    public override string ToString() => $"{Title} by {Author.ToShortString()} ({PublishDate.ToShortDateString()})";
}