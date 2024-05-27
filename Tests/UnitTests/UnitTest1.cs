namespace Kasvang.RankedChoiceVoting.UnitTests;

public class UnitTest1
{
    [Fact]
    public void MajorityWinsTest()
    {
        // Arrange
        var candidates = new List<string> { "A", "B", "C", "D" };

        var votes = new List<Vote>
        {
            new Vote(new List<string> { "A", "B", "C", "D" }),
            new Vote(new List<string> { "B", "C", "D", "A" }),
            new Vote(new List<string> { "C", "D", "A", "B" }),
            new Vote(new List<string> { "C", "D", "A", "B" })
        };

        // Act
        var election = new Election(candidates, votes);
        election.RunElection();

        // Assert
        Assert.Equal("C", election.Winner);
    }

    [Fact]
    public void NoMajorityTest()
    {
        // Arrange
        var candidates = new List<string> { "A", "B", "C", "D" };

        var votes = new List<Vote>
        {
            new Vote(new List<string> { "A", "B", "C", "D" }),
            new Vote(new List<string> { "B", "C", "D", "A" }),
            new Vote(new List<string> { "C", "D", "A", "B" }),
            new Vote(new List<string> { "D", "C", "A", "B" })
        };

        // Act
        var election = new Election(candidates, votes);
        election.RunElection();

        // Assert
        Assert.Equal("C", election.Winner);
    }

    [Fact]
    public void WinnerByEliminationTest()
    {
        // Arrange
        var candidates = new List<string> { "A", "B", "C", "D" };

        var votes = new List<Vote>
        {
            new Vote(new List<string> { "A", "D", "C", "D" }),
            new Vote(new List<string> { "C", "C", "D", "A" }),
            new Vote(new List<string> { "C", "D", "A", "B" }),
            new Vote(new List<string> { "D", "C", "A", "B" }),
            new Vote(new List<string> { "D", "C", "A", "B" })
        };

        // Act
        var election = new Election(candidates, votes);
        election.RunElection();

        // Assert
        Assert.Equal("D", election.Winner);
    }
}
