using Microsoft.VisualStudio.TestTools.UnitTesting;

using Research.Models;
using Research.Enums;
using Research.Collections;

namespace ResearchTest.CollectionsTests;

[TestClass]
public class ResearchTeamCollectionTests
{
    private static readonly ResearchTeamCollection _researchTeamCollection = new();

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        var researchTeam1 = GetRandomResearchTeam();
        var researchTeam2 = GetRandomResearchTeam();
        var researchTeam3 = GetRandomResearchTeam();

        _researchTeamCollection.AddResearchTeams(researchTeam1, researchTeam2, researchTeam3);
    }

    [TestMethod]
    public void TestMinimumRegistrationNumber()
    {
        var expected = _researchTeamCollection.Min(x => x.RegistrationNumber);

        var actual = _researchTeamCollection.MinimumRegistrationNumber;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestResearchTeamWithinTwoYears()
    {
        var expected = _researchTeamCollection.Where(x => x.TimeFrame == TimeFrame.TwoYears);

        var actual = _researchTeamCollection.ResearchTeamWithinTwoYears;

        Assert.AreEqual(actual.Count(), expected.Count());
        CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
    }

    [TestMethod]
    [Ignore("Until fixed bug in dotnet runtime")]
    public void TestNGroup()
    {
        int count = 2;
        var expected = _researchTeamCollection.GroupBy(
            keySelector: x => x.Publications.Count == count
        );
        var actual = _researchTeamCollection.NGroup(count);

        CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
    }

    private static ResearchTeam GetRandomResearchTeam()
    {
        var random = new Random();

        var researchTeam = new ResearchTeam
        {
            Topic = $"Topic {random.Next(0, 100)}",
            Organization = $"Organization {random.Next(0, 100)}",
            RegistrationNumber = random.Next(0, 100),
            TimeFrame = (TimeFrame)random.Next(0, 3),
        };

        var person = new Person(
            $"First name {random.Next(0, 100)}",
            $"Last name {random.Next(0, 100)}",
            new DateTime(random.Next(1900, 2021), random.Next(1, 12), random.Next(1, 28))
        );

        researchTeam.AddMembers(person);

        researchTeam.AddPapers(
            new Paper(
                $"Paper {random.Next(0, 100)}",
                person,
                new DateTime(random.Next(1900, 2021), random.Next(1, 12), random.Next(1, 28))
            )
        );

        return researchTeam;
    }
}
