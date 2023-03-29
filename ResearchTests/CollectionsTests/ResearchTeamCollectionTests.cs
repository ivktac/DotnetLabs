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
    public void TestNGroup()
    {
        int count = 2;
        var expected = _researchTeamCollection.GroupBy(
            keySelector: x => x.Publications.Count == count
        ).SelectMany(x => x).ToList();
        var actual = _researchTeamCollection.NGroup(count);

        CollectionAssert.AreEqual(expected.ToList(), actual.ToList());
    }

    [TestMethod]
    public void TestTeamListHandler()
    {
        var handler = new ResearchTeamCollection.TeamListHandler((source, args) =>
        {
            Assert.AreEqual(_researchTeamCollection, source);
            Assert.AreEqual(_researchTeamCollection.Name, args.CollectionName);

            _researchTeamCollection.AddResearchTeams(GetRandomResearchTeam());
            Assert.AreEqual("Added element to collection", args.ChangeType);

            _researchTeamCollection.InsertAt(0, GetRandomResearchTeam());
            Assert.AreEqual("Inserted element to collection", args.ChangeType);
            Assert.AreEqual(0, args.IndexElement);

            _researchTeamCollection.Remove(3);
            Assert.AreEqual("Removed element from collection", args.ChangeType);
            Assert.AreEqual(3, args.IndexElement);
        });
    }

    private static ResearchTeam GetRandomResearchTeam()
    {
        var random = new Random();

        var researchTeam = new ResearchTeam
        {
            Topic = $"Topic {random.Next(1, 100)}",
            Organization = $"Organization {random.Next(1, 100)}",
            RegistrationNumber = random.Next(1, 100),
            TimeFrame = (TimeFrame)random.Next(1, 3),
        };

        var person = new Person(
            $"First name {random.Next(1, 100)}",
            $"Last name {random.Next(1, 100)}",
            new DateTime(random.Next(1900, 2021), random.Next(1, 12), random.Next(1, 28))
        );

        researchTeam.AddMembers(person);

        researchTeam.AddPapers(
            new Paper(
                $"Paper {random.Next(1, 100)}",
                person,
                new DateTime(random.Next(1900, 2021), random.Next(1, 12), random.Next(1, 28))
            )
        );

        return researchTeam;
    }
}
