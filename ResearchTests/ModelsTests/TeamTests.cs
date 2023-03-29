using Microsoft.VisualStudio.TestTools.UnitTesting;

using Research.Models;

namespace ResearchTest.ModelsTests;

[TestClass]
public class TeamTests
{
    [TestMethod]
    public void TestThatSetRegistrationNumberThrowsException()
    {
        var team = new Team("Organization", 1);
        Assert.ThrowsException<ArgumentException>(() => team.RegistrationNumber = -1);
    }
}
