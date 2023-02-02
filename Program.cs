using Research;

class Program
{
    static void Main(string[] args)
    {
        var team1 = new Team { Organization = "Microsoft Dev Team", RegistrationNumber = 1 };
        var team2 = new Team { Organization = "Microsoft Dev Team", RegistrationNumber = 1 };

        Console.WriteLine($"ReferenceEquals: {ReferenceEquals(team1, team2)}");
        Console.WriteLine($"team1 == team2: {team1 == team2}");
        Console.WriteLine($"HashCode team1: {team1.GetHashCode()}, HashCode team2: {team2.GetHashCode()}");

        try
        {
            Team team3 = new() { Organization = "Microsoft Dev Team", RegistrationNumber = 0 };
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine(ex.Message);
        }


        var microsoftResearch = new ResearchTeam
        {
            Topic = "C# 9.0",
            Organization = "Microsoft Research",
            RegistrationNumber = 1,
            TimeFrame = TimeFrame.Year,
            Members = new List<Person>
                {
                    new Person("Anders", "Hejlsberg", new DateTime(1960, 12, 2)),
                    new Person("Mads", "Torgersen", new DateTime(1975, 1, 1)),
                }
        };
        microsoftResearch.AddPapers(new Paper("C# 9.0", new Person("Anders", "Hejlsberg", new DateTime(1960, 12, 2)), new DateTime(2020, 8, 4)));

        Console.WriteLine(microsoftResearch.ToString());

        Console.WriteLine($"Microsoft Research's Team: {microsoftResearch.Team}");

        var microsoftResearchCopy = (ResearchTeam)microsoftResearch.DeepCopy();

        microsoftResearch.AddPapers(new Paper("C# 10.0", new Person("Anders", "Hejlsberg", new DateTime(1960, 12, 2)), new DateTime(2021, 8, 4)));

        Console.WriteLine($"Microsoft Research's Team:\n{microsoftResearch}");
        Console.WriteLine($"Microsoft Research's Team Copy:\n{microsoftResearchCopy}");

        try
        {
            Console.WriteLine("Persons without publications:");
            foreach (var person in microsoftResearch.GetPersonsWithNoPublications())
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("Publications within last two years");

            foreach (var paper in microsoftResearch.GetPapersWithinLastYears(2))
            {
                Console.WriteLine(paper);
            }

        }
        catch (NotImplementedException ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}