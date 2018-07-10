using System;
using SeasonStats.Model;
using Xunit;

namespace SeasonStats.Model.Tests
{
    public class MatchTests
    {
        [Fact]
        public static void AddingSetWithDifferentPlayersThrowsException()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Player player3 = new Player("3");
            Match match = new Match();
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            Assert.Throws<Exception>(() => match.AddSet(new Set(player1, player3, 2, 11)));
        }

        [Fact]
        public static void AddingMoreThanSevenSetsThrowsException()
        {
            Player player1 = new Player("Gleb");
            Player player2 = new Player("NotGleb");
            Match match = new Match();
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 1, 11));
            match.AddSet(new Set(player1, player2, 1, 11));
            match.AddSet(new Set(player1, player2, 1, 11));
            Assert.Throws<Exception>(() => match.AddSet(new Set(player1, player2, 1, 11)));
        }

        [Fact]
        public static void AddingNullSetThrowsArgumentNullException()
        {
            Set set = null;
            Match match = new Match();
            Assert.Throws<ArgumentNullException>(() => match.AddSet(set));
        }

        [Fact]
        public static void CountFinalScore()
        {
            Player player1 = new Player("Gleb");
            Player player2 = new Player("NotGleb");
            Match match = new Match();
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 11, 2));
            match.AddSet(new Set(player1, player2, 1, 11));
            match.AddSet(new Set(player1, player2, 1, 11));

            Assert.Equal(4, match.Player1Score);
            Assert.Equal(2, match.Player2Score);
        }
    }
}
