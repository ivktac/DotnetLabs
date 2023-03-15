using Research.Models;

namespace Research.Extensions;

public static class ConsoleMethods
{
    public static Person? ReadPerson()
    {
        Console.WriteLine("Enter person's name, surname and date of birth (delimeter is space):");
        string[] input =
            Console.ReadLine()?.Split(' ') ?? new string[3] { "John", "Doe", "1990-01-01" };

        if (input.Length != 3)
        {
            return null;
        }

        string name = input[0];
        string surname = input[1];
        DateTime dateOfBirth = DateTime.Parse(input[2]);

        return new Person(name, surname, dateOfBirth);
    }

    public static Paper? ReadPaper()
    {
        Console.WriteLine("Enter paper's title, author and publish date (delimeter is space):");
        string[] input =
            Console.ReadLine()?.Split(' ') ?? new string[3] { "Paper", "John Doe", "2020-01-01" };

        if (input.Length != 3)
        {
            return null;
        }

        string title = input[0];
        Person? author = ReadPerson();
        DateTime publishDate = DateTime.Parse(input[2]);

        if (author is null)
        {
            return null;
        }

        return new Paper(title, author, publishDate);
    }
}
