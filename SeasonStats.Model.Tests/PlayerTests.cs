using System;
using SeasonStats.Model;
using Xunit;


namespace SeasonStats.Model.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void ConstructorShouldThrowArgumentNullException()
        {
            string str = null;

            Assert.Throws<ArgumentNullException>(() => new Player(str));
        }

        [Fact]
        public void EqualsReturnsTrueForPlayersWithEqualNames()
        {
            var player1 = new Player("Gleb");
            var player2 = new Player("Gleb");

            Assert.Equal(player1, player2);
        }

        [Fact]
        public void EqualsReturnsFalseForPlayersWithDifferentNames()
        {
            var player1 = new Player("Gleb");
            var player2 = new Player("Not Gleb");

            Assert.NotEqual(player1, player2);
        }
    }
}