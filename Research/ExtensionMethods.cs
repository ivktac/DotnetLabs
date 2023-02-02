namespace Research;

internal static class PaperExtension
{
    internal static Paper[][] GenerateJaggedArray(int size)
    {
        int n = (int)Math.Ceiling((Math.Sqrt(8 * size + 1) - 1) / 2);
        Paper[][] papers = new Paper[n][];

        int actualSize = 0;
        for (int i = 0; i < n - 1; i++)
        {
            papers[i] = GenerateArray(i + 1);
            actualSize += papers[i].Length;
        }

        papers[n - 1] = GenerateArray(size - actualSize);

        return papers;
    }

    internal static Paper[,] GenerateMatrix(int rows, int columns)
    {
        Paper[,] papers = new Paper[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                papers[i, j] = new Paper();
            }
        }

        return papers;
    }

    internal static Paper[] GenerateArray(int size)
    {
        Paper[] papers = new Paper[size];

        for (int i = 0; i < size; i++)
        {
            papers[i] = new Paper();
        }

        return papers;
    }

    internal static Person? ReadPerson()
    {
        Console.WriteLine("Enter person's name, surname and date of birth (delimeter is space):");
        string[] input = Console.ReadLine()?.Split(' ') ?? new string[3] { "John", "Doe", "1990-01-01" };

        if (input.Length != 3)
        {
            return null;
        }

        string name = input[0];
        string surname = input[1];
        DateTime dateOfBirth = DateTime.Parse(input[2]);

        return new Person(name, surname, dateOfBirth);
    }

    internal static Paper? ReadPaper()
    {
        Console.WriteLine("Enter paper's title, author and publish date (delimeter is space):");
        string[] input = Console.ReadLine()?.Split(' ') ?? new string[3] { "Paper", "John Doe", "2020-01-01" };

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