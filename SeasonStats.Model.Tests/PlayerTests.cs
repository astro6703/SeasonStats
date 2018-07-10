using System;
using SeasonStats.Model;
using Xunit;


namespace SeasonStats.Model.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayersAreEqual()
        {
            Player player1 = new Player("Gleb");
            Assert.True(player1.Equals(new Player("Gleb")));
        }

        [Fact]
        public void PlayersAreNotEqual()
        {
            Player player1 = new Player("Gleb");
            Assert.False(player1.Equals(new Player("NotGleb")));
        }
    }
}