using Microsoft.VisualStudio.TestTools.UnitTesting;

using Research.Models;

namespace Research.Tests.ModelsTests;

[TestClass]
public class TeamTest
{
    [TestMethod]
    public void TestThatSetRegistrationNumberThrowsException()
    {
        var team = new Team("Organization", 1);
        Assert.ThrowsException<ArgumentException>(() => team.RegistrationNumber = -1);
    }
}
