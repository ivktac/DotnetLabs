namespace Research;

internal static class PaperExtension
{
    internal static Paper[][] ToJaggedArray(Paper[,] twoDimensionalArray)
    {
        int rowFirstIndex = twoDimensionalArray.GetLowerBound(0);
        int rowLastIndex = twoDimensionalArray.GetUpperBound(0);
        int numberOfRows = rowLastIndex + 1;

        int columnFirstIndex = twoDimensionalArray.GetLowerBound(1);
        int columnLastIndex = twoDimensionalArray.GetUpperBound(1);
        int numberOfColumns = columnLastIndex + 1;

        Paper[][] jaggedArray = new Paper[numberOfRows][];
        for (int i = rowFirstIndex; i <= rowLastIndex; i++)
        {
            jaggedArray[i] = new Paper[numberOfColumns];
            for (int j = columnFirstIndex; j <= columnLastIndex; j++)
            {
                jaggedArray[i][j] = twoDimensionalArray[i, j];
            }
        }
        return jaggedArray;
    }

    internal static Paper[,] GetMatrix(int rows, int columns)
    {
        Paper[,] papers2D = new Paper[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                papers2D[i, j] = new Paper();
            }
        }

        return papers2D;
    }

    internal static Paper[] ToArray(Paper[,] twoDimensionalArray)
    {
        Paper[] papers = new Paper[twoDimensionalArray.Length];
        for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
            {
                papers[i * twoDimensionalArray.GetLength(1) + j] = twoDimensionalArray[i, j];
            }
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

        if (author == null)
        {
            return null;
        }

        return new Paper(title, author, publishDate);
    }
}