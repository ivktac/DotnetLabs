using Research.Enums;
using Research.Models;
using Research.Collections;
using Reseach.Services;

var researchTeamsProgrammingLanguages = new ResearchTeamCollection("Programming Languages");

var teamsProgrammingLanguagesJournal = new TeamsJournal();

researchTeamsProgrammingLanguages.ResearchTeamAdded += teamsProgrammingLanguagesJournal.OnResearchTeamAddedOrInserted;
researchTeamsProgrammingLanguages.ResearchTeamInserted +=
    teamsProgrammingLanguagesJournal.OnResearchTeamAddedOrInserted;

var anders = new Person("Anders", "Hejlsberg", new DateTime(1955, 12, 2));

var c_sharp = new ResearchTeam
{
    Topic = "C#",
    Organization = "Microsoft",
    RegistrationNumber = 1,
    TimeFrame = TimeFrame.Long,
};

c_sharp.AddMembers(
    anders,
    new Person("Bill", "Gates", new DateTime(1955, 10, 28)),
    new Person("Steve", "Jobs", new DateTime(1955, 2, 24))
);

c_sharp.AddPapers(new Paper("C# 1.0", anders, new DateTime(2000, 12, 1)));

c_sharp.AddPapers(new Paper("C# 2.0", anders, new DateTime(2005, 12, 1)));

c_sharp.AddPapers(new Paper("C# 3.0", anders, new DateTime(2008, 12, 1)));

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

researchTeamsProgrammingLanguages.AddResearchTeams(rust, c_sharp);

researchTeamsProgrammingLanguages.InsertAt(
    0,
    new ResearchTeam
    {
        Topic = "C++",
        Organization = "Bjarne Stroustrup",
        RegistrationNumber = 3,
        TimeFrame = TimeFrame.TwoYears,
    }
);

Console.WriteLine(teamsProgrammingLanguagesJournal.ToString());

var researchTeamBooks = new ResearchTeamCollection("Research Team Books");

var teamsBookJournal = new TeamsJournal();

researchTeamBooks.ResearchTeamAdded += teamsBookJournal.OnResearchTeamAddedOrInserted;
researchTeamBooks.ResearchTeamInserted += teamsBookJournal.OnResearchTeamAddedOrInserted;

researchTeamBooks.AddResearchTeams(
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

researchTeamBooks.AddResearchTeams(
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

researchTeamBooks.InsertAt(
    3,
    new ResearchTeam
    {
        Topic = "C++",
        Organization = "Bjarne Stroustrup",
        RegistrationNumber = 3,
        TimeFrame = TimeFrame.Long,
    }
);

researchTeamBooks.Remove(4);

researchTeamBooks.InsertAt(
    4,
    new ResearchTeam
    {
        Topic = "C++",
        Organization = "Bjarne Stroustrup",
        RegistrationNumber = 3,
        TimeFrame = TimeFrame.Long,
    }
);

Console.WriteLine(teamsBookJournal.ToString());
