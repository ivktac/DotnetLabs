using Research;

class Program
{
    static void Main(string[] args)
    {
        ResearchTeam microsoftResearch = new ResearchTeam("C#", "Microsoft", 1, TimeFrame.Long);

        Console.WriteLine(microsoftResearch.ToShortString());

        Console.WriteLine($"Is research team working on C# for a year? {microsoftResearch[TimeFrame.Year]}");
        Console.WriteLine($"Is research team working on C# for two years? {microsoftResearch[TimeFrame.TwoYears]}");
        Console.WriteLine($"Is research team working on C# for long? {microsoftResearch[TimeFrame.Long]}");

        ResearchTeam oracleResearch = new()
        {
            Topic = "Java",
            Organization = "Oracle",
            RegistrationNumber = 2,
            TimeFrame = TimeFrame.TwoYears,
        };

        Console.WriteLine(oracleResearch.ToString());

        oracleResearch.AddPapers(
            new Paper[]
            {
                new Paper("Java 7.0", new Person("John", "Doe", new DateTime(1990, 1, 1)), new DateTime(2022, 1, 1)),
                new Paper("Java 6.0", new Person("Jane", "Doe", new DateTime(1990, 1, 1)), new DateTime(2021, 1, 1)),
                new Paper("Java 5.0", new Person("John", "Doe", new DateTime(1990, 1, 1)), new DateTime(2020, 1, 1))
            }
        );

        Console.WriteLine(oracleResearch.ToString());

        Console.WriteLine($"Last publication: {oracleResearch.LastPublication}");

        Console.WriteLine("Enter nRows and nColumns (by delimeters):");

        char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

        string[] input = Console.ReadLine()?.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries) ?? new string[2] { "3", "2" };

        int rows = int.Parse(input[0]);
        int columns = int.Parse(input[1]);

        Paper[,] papers = PaperExtension.GenerateMatrix(rows, columns);
        int timeElapsed;

        timeElapsed = GetTimeElapsed(() =>
        {
            DateTime maxDate = papers[0, 0].PublishDate;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (papers[i, j].PublishDate > maxDate)
                    {
                        maxDate = papers[i, j].PublishDate;
                    }
                }
            }
        });

        Console.WriteLine($"Time elapsed of find max publish date in matrix with {rows} rows and {columns} columns: {timeElapsed} ms");


        Paper[] papersArray = PaperExtension.GenerateArray(rows * columns);

        timeElapsed = GetTimeElapsed(() =>
        {
            DateTime maxDate = papersArray[0].PublishDate;
            for (int i = 0; i < papersArray.Length; i++)
            {
                if (papersArray[i].PublishDate > maxDate)
                {
                    maxDate = papersArray[i].PublishDate;
                }
            }
        });


        Console.WriteLine($"Time elapsed of one-dimensional array with {rows * columns} columns: {timeElapsed} ms");

        Paper[][] papersJaggedArray = PaperExtension.GenerateJaggedArray(rows * columns);

        timeElapsed = GetTimeElapsed(() =>
        {
            DateTime maxDate = papersJaggedArray[0][0].PublishDate;
            for (int i = 0; i < papersJaggedArray.Length; i++)
            {
                for (int j = 0; j < papersJaggedArray[i].Length; j++)
                {
                    if (papersJaggedArray[i][j].PublishDate > maxDate)
                    {
                        maxDate = papersJaggedArray[i][j].PublishDate;
                    }
                }
            }
        });

        Console.WriteLine($"Time elapsed of find max publish date in jagged array with size {rows * columns}: {timeElapsed} ms");
    }

    private static int GetTimeElapsed(Action action)
    {
        int startTime = Environment.TickCount;

        action();

        int endTime = Environment.TickCount;

        return endTime - startTime;
    }
}