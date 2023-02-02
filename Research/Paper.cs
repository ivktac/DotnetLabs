namespace Research;

class Paper
{
    public string Title { get; set; }
    public Person Author { get; set; }
    public DateTime PublishDate { get; set; }

    public Paper(string title, Person author, DateTime publishDate)
    {
        Title = title;
        Author = author;
        PublishDate = publishDate;
    }

    public Paper()
    {
        Title = "Untitled";
        Author = new Person();
        PublishDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Title} by {Author.ToShortString()} ({PublishDate.ToShortDateString()})";
    }
}