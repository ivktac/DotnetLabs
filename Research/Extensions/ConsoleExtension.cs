using Research.Models;

namespace Research.Extensions;

public class ConsoleExtension
{
    public ConsoleExtension() { }

    /// <summary>
    /// Reads a person from the console.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when input format is invalid.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the person is null.</exception>
    public Person ReadPerson()
    {
        Console.WriteLine(
            "Enter name, surname and date of birth (in format yyyy-MM-dd) of person, data should be separated by space, comma or semicolon: "
        );

        char[] delimiters = { ' ', ',', ';' };
        string[] input =
            Console.ReadLine()?.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            ?? new string[0];

        if (input.Length != 3)
        {
            throw new ArgumentException(
                $"Invalid input format. Expected name, surname, and date of birth."
            );
        }

        string name = input[0];
        string surname = input[1];

        if (!DateTime.TryParse(input[2], out var birthday))
        {
            throw new ArgumentException($"Invalid date of birth format. Expected yyyy-MM-dd.");
        }

        var person = new Person(name, surname, birthday);

        if (person is null)
        {
            throw new ArgumentNullException(nameof(person));
        }

        return person;
    }

    /// <summary>
    /// Reads a paper from the console.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when input format is invalid.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the paper is null.</exception>
    public Paper ReadPaper()
    {
        Console.WriteLine(
            "Enter title, date of publication (in format yyyy-MM-dd), data should be separated by space, comma or semicolon: "
        );

        char[] delimiters = { ' ', ',', ';' };
        string[] input =
            Console.ReadLine()?.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            ?? new string[0];

        if (input.Length != 2)
        {
            throw new ArgumentException(
                $"Invalid input format. Expected title, author, and date of publication."
            );
        }

        string title = input[0];
        Person author = ReadPerson();
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author));
        }

        if (!DateTime.TryParse(input[1], out var publicationDate))
        {
            throw new ArgumentException(
                $"Invalid date of publication format. Expected yyyy-MM-dd."
            );
        }

        return new Paper(title, author, publicationDate);
    }
}
