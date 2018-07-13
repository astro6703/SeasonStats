using System;
using SeasonStats.Model;
using Xunit;

namespace SeasonStats.Model.Tests
{
    public class MatchTests
    {
        [Fact]
        public void SetUpMatchPlayersCorrectly()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var match = new Match(3);

            match.AddSet(new Set(player1, player2, 11, 5));

            Assert.Equal(player1, match.Player1);
            Assert.Equal(player2, match.Player2);
        }

        [Fact]
        public void AddSetWithDifferentPlayersThrowsException()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var player3 = new Player("3");
            var match = new Match(5);

            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            var exception = Record.Exception(() => match.AddSet(new Set(player1, player3, 2, 11)));

            Assert.IsType<Exception>(exception);
            Assert.Equal("Match players doesn't match to match players", exception.Message);
        }

        [Fact]
        public void AddSetWithValidArgumentThrowsException()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var match = new Match(3);

            var exception = Record.Exception(() => match.AddSet(new Set(player1, player2, 5, 5)));

            Assert.IsType<Exception>(exception);
            Assert.Equal("Given set is not valid", exception.Message);
        }

        [Fact]
        public void ConstructorShouldThrowArgumentException()
        {
            var exception = Record.Exception(() => new Match(2));

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("Wrong maximal number of sets", exception.Message);
        }

        [Fact]
        public void AddSecondSetToOneSetMatchShouldThrowException()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");
            var match = new Match(1);

            match.AddSet(new Set(player1, player2, 5, 11));
            var exception = Record.Exception(() => match.AddSet(new Set(player1, player2, 11, 5)));

            Assert.IsType<Exception>(exception);
            Assert.Equal("The match has already been finished", exception.Message);
        }

        [Fact]
        public void AddSetWithNullArgumentThrowsArgumentNullException()
        {
            Set set = null;
            var match = new Match(3);

            Assert.Throws<ArgumentNullException>(() => match.AddSet(set));
        }

        [Fact]
        public void AddSetThrowsExceptonWhenAddingSetToAFinishedMatch()
        {
            var player1 = new Player("Gleb");
            var player2 = new Player("Not Gleb");
            var match = new Match(5);

            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            var exception = Record.Exception(() => match.AddSet(new Set(player1, player2, 11, 2)));

            Assert.IsType<Exception>(exception);
            Assert.Equal("The match has already been finished", exception.Message);
        }

        [Fact]
        public void PlayersScorePropertiesShouldReturnPreciseScore()
        {
            var player1 = new Player("Gleb");
            var player2 = new Player("Not Gleb");
            var match = new Match(7);

            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 1, 11));
            match.AddSet(new Set(player1, player2, 1, 11));
            match.AddSet(new Set(player1, player2, 1, 11));
            match.AddSet(new Set(player1, player2, 11, 1));

            Assert.Equal(4, match.Player1Score);
            Assert.Equal(3, match.Player2Score);
        }
    }
}
