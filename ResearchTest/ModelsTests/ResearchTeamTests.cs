using Microsoft.VisualStudio.TestTools.UnitTesting;

using Research.Enums;
using Research.Models;

namespace ResearchTest.ModelsTests;

[TestClass]
public class ResearchTeamTests
{
    private static ResearchTeam _researchTeam = default!;

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _researchTeam = new ResearchTeam("Topic", "Organization", 1, TimeFrame.Long);
        _researchTeam.AddMembers(new Person("Name", "Surname", new DateTime(2000, 1, 1)));
        _researchTeam.AddPapers(
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
        var actual = _researchTeam.GetPersonsWithNoPublications();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void TestGetPersonWithPublications()
    {
        var expected = new List<Person> { new Person("Name", "Surname", new DateTime(2000, 1, 1)) };
        var actual = _researchTeam.GetPersonWithPublications();
        CollectionAssert.AreEqual(expected, actual.ToList());
    }

    [TestMethod]
    public void TestGetPersonWithMoreOnePublications()
    {
        _researchTeam.AddPapers(
            new Paper(
                "Title",
                new Person("Name", "Surname", new DateTime(2000, 1, 1)),
                new DateTime(2000, 1, 1)
            )
        );
        var expected = new List<Person> { new Person("Name", "Surname", new DateTime(2000, 1, 1)) };
        var actual = _researchTeam.GetPersonWithPublications(1);
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
