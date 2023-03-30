using Research.Enums;
using Research.Models;
using Research.Collections;
using Research.Extensions;

var programmingLanguages = new ResearchTeamCollection("Programming Languages");

var programmingLanguagesJournal = new TeamsJournal();

programmingLanguages.ResearchTeamAdded +=
    programmingLanguagesJournal.OnResearchTeamAddedOrInserted;
programmingLanguages.ResearchTeamInserted +=
    programmingLanguagesJournal.OnResearchTeamAddedOrInserted;

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

programmingLanguages.InsertAt(
    0,
    new ResearchTeam
    {
        Topic = "C++",
        Organization = "Bjarne Stroustrup",
        RegistrationNumber = 3,
        TimeFrame = TimeFrame.TwoYears,
    }
);

Console.WriteLine(programmingLanguagesJournal.ToString());

var books = new ResearchTeamCollection("Research Team Books");

var booksJournal = new TeamsJournal();

books.ResearchTeamAdded += booksJournal.OnResearchTeamAddedOrInserted;
books.ResearchTeamInserted += booksJournal.OnResearchTeamAddedOrInserted;

books.AddResearchTeams(
    new ResearchTeam
    {
        Topic = "C#",
        Organization = "Microsoft",
        RegistrationNumber = 1,
        TimeFrame = TimeFrame.Long,
    },
    new ResearchTeam
    {
        Topic = "Rust",
        Organization = "Mozilla",
        RegistrationNumber = 2,
        TimeFrame = TimeFrame.TwoYears,
    }
);

books.AddResearchTeams(
    new ResearchTeam
    {
        Topic = "C#",
        Organization = "Microsoft",
        RegistrationNumber = 1,
        TimeFrame = TimeFrame.Long,
    },
    new ResearchTeam
    {
        Topic = "Rust",
        Organization = "Mozilla",
        RegistrationNumber = 2,
        TimeFrame = TimeFrame.TwoYears,
    }
);

books.InsertAt(
    3,
    new ResearchTeam
    {
        Topic = "C++",
        Organization = "Bjarne Stroustrup",
        RegistrationNumber = 3,
        TimeFrame = TimeFrame.Long,
    }
);

books.Remove(4);

books.InsertAt(
    4,
    new ResearchTeam
    {
        Topic = "C++",
        Organization = "Bjarne Stroustrup",
        RegistrationNumber = 3,
        TimeFrame = TimeFrame.Long,
    }
);

Console.WriteLine(booksJournal.ToString());
