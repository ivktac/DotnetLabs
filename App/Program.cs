using Research.Enums;
using Research.Models;
using Research.Collections;

var programmingLanguages = new ResearchTeamCollection();

var anders = new Person("Anders", "Hejlsberg", new DateTime(1955, 12, 2));

var csharp = new ResearchTeam
{
    Topic = "C#",
    Organization = "Microsoft",
    RegistrationNumber = 1,
    TimeFrame = TimeFrame.Long,
};

csharp.AddMembers(
    anders,
    new Person("Bill", "Gates", new DateTime(1955, 10, 28)),
    new Person("Steve", "Jobs", new DateTime(1955, 2, 24))
);

csharp.AddPapers(new Paper("C# 1.0", anders, new DateTime(2000, 12, 1)));

csharp.AddPapers(new Paper("C# 2.0", anders, new DateTime(2005, 12, 1)));

csharp.AddPapers(new Paper("C# 3.0", anders, new DateTime(2008, 12, 1)));

var rust = new ResearchTeam
{
    Topic = "Rust",
    Organization = "Mozilla",
    RegistrationNumber = 2,
    TimeFrame = TimeFrame.TwoYears,
};

rust.AddMembers(
    new Person("Graydon", "Hoare", new DateTime(1986, 12, 2)),
    new Person("Steve", "Jobs", new DateTime(1955, 2, 24))
);

rust.AddPapers(
    new Paper(
        "Rust 1.0",
        new Person("Graydon", "Hoare", new DateTime(1986, 12, 2)),
        new DateTime(2015, 12, 1)
    )
);

rust.AddPapers(
    new Paper(
        "Rust 2.0",
        new Person("Graydon", "Hoare", new DateTime(1986, 12, 2)),
        new DateTime(2018, 12, 1)
    )
);

programmingLanguages.AddResearchTeams(rust, csharp);

Console.WriteLine(programmingLanguages.ToString());

Console.WriteLine("Research team sorted by registration number:");

programmingLanguages.SortByRegistartionNumber();

Console.WriteLine(programmingLanguages.ToString());

Console.WriteLine("Research team sorted by topic:");

programmingLanguages.SortByTopic();

Console.WriteLine(programmingLanguages.ToString());

Console.WriteLine("Research team sorted by publications count:");

programmingLanguages.SortByPublicationsCount();

Console.WriteLine(programmingLanguages.ToString());

var minimumRegistrationNumber = programmingLanguages.MinimumRegistrationNumber;

Console.WriteLine($"Minimum registration number: {minimumRegistrationNumber}");

var filteredResearchTeamCollection = programmingLanguages.ResearchTeamWithinTwoYears;

Console.WriteLine("Research team within two years:");

foreach (var filteredReserchTeam in filteredResearchTeamCollection)
{
    Console.WriteLine(filteredReserchTeam.ToString());
}

var groupedResearchTeamCollection = programmingLanguages.NGroup(2);

Console.WriteLine("Grouped research team collection:");

foreach (var groupedResearchTeam in groupedResearchTeamCollection)
{
    Console.WriteLine(groupedResearchTeam.ToString());
}

var example = new TestCollections(1_000_000);

example.InitializeDefaultValues();

void CompareElapsedTimeOfSearch(TestCollections collection, int index)
{
    var researchTeam = collection[index];

    int[] time = new int[4];
    
    time[0] = collection.GetElapsedTimeOfSearchInTeams(researchTeam);
    time[1] = collection.GetTimeElapsedOfSearchInTopics(researchTeam);
    time[2] = collection.GetTimeElapsedOfSearchInResearchTeams(researchTeam);
    time[3] = collection.GetTimeElapsedOfSearchInResearchTeamsByTopic(researchTeam);

    Console.WriteLine($"Search {index} element in collection with {collection.Count} elements");
    Console.WriteLine($"Time elapsed of search in teams: {time[0]}ms");
    Console.WriteLine($"Time elapsed of search in topics: {time[1]}ms");
    Console.WriteLine($"Time elapsed of search in research teams: {time[2]}ms");
    Console.WriteLine(
        $"Time elapsed of search in dictionary by research teams by topic: {time[3]}ms"
    );
}

CompareElapsedTimeOfSearch(example, 0);
CompareElapsedTimeOfSearch(example, example.Count / 2);
CompareElapsedTimeOfSearch(example, example.Count - 1);
CompareElapsedTimeOfSearch(example, int.MaxValue - 1);