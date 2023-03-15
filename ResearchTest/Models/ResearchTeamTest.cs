using Microsoft.VisualStudio.TestTools.UnitTesting;
using Research.Enums;

namespace Research.Models;

[TestClass]
public class ResearchTeamTest
{
    private static ResearchTeam researchTeam = default!;

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        researchTeam = new ResearchTeam("Topic", "Organization", 1, TimeFrame.Long);
        researchTeam.AddMembers(new Person("Name", "Surname", new DateTime(2000, 1, 1)));
        researchTeam.AddPapers(
            new Paper(
                "Title",
                new Person("Name", "Surname", new DateTime(2000, 1, 1)),
                new DateTime(2000, 1, 1)
            )
        );
    }

    [TestMethod]
    public void TestGetPersonsWithNoPublications()
    {
        var expected = new List<Person>();
        var actual = researchTeam.GetPersonsWithNoPublications();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void TestGetPersonWithPublications()
    {
        var expected = new List<Person> { new Person("Name", "Surname", new DateTime(2000, 1, 1)) };
        var actual = researchTeam.GetPersonWithPublications();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void TestGetPersonWithMoreOnePublications()
    {
        researchTeam.AddPapers(
            new Paper(
                "Title",
                new Person("Name", "Surname", new DateTime(2000, 1, 1)),
                new DateTime(2000, 1, 1)
            )
        );
        var expected = new List<Person> { new Person("Name", "Surname", new DateTime(2000, 1, 1)) };
        var actual = researchTeam.GetPersonWithPublications(1);
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void TestGetPapersInLastYear()
    {
        var researchTeam = new ResearchTeam("Topic", "Organization", 1, TimeFrame.Long);
        researchTeam.AddPapers(
            new Paper(
                "Title",
                new Person("Name", "Surname", new DateTime(2000, 1, 1)),
                new DateTime(2000, 1, 1)
            )
        );
        researchTeam.AddPapers(
            new Paper(
                "Title",
                new Person("Name", "Surname", new DateTime(2000, 1, 1)),
                new DateTime(2000, 1, 1)
            )
        );
        var expected = new List<Paper>();
        var actual = researchTeam.GetPapersInLastYear();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }
}
