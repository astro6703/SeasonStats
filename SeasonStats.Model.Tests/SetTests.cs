using System;
using SeasonStats.Model;
using Xunit;

namespace SeasonStats.Model.Tests
{
    public class SetTests
    {
        [Fact]
        public void ThrowsArgumentNullException()
        {
            Player player1 = null;
            Player player2 = new Player("2");
            Assert.Throws<ArgumentNullException>(() => new Set(player1, player2, 5, 5));
        }

        [Fact]
        public void ThrowsArgumentException()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.Throws<ArgumentException>(() => new Set(player1, player2, -5, 5));
        }

        [Fact]
        public void DoesntThrowAnyExceptions()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.Null(Record.Exception(() => new Set(player1, player2, 0, 11)));
        }

        [Fact]
        public void ReturnTrueForSetWithNoOvertimeAndScoreDifferenceTwo()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.True(new Set(player1, player2, 11, 9).IsValid());
        }
        
        [Fact]
        public void ReturnTrueForSetWithNoOvertimeAndScoreDifferenceMoreThanTwo()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.True(new Set(player1, player2, 11, 7).IsValid());
        }

        [Fact]
        public void ReturnFalseForSetWithNoOvertimeAndScoreDifferenceLessThanTwo()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.False(new Set(player1, player2, 11, 10).IsValid());
        }

        [Fact]
        public void ReturnTrueForSetWithOvertimeWithOneOfScoresTenAndScoreDiffrenceTwo()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.True(new Set(player1, player2, 10, 12).IsValid());
        }

        [Fact]
        public void ReturnTrueForSetWithOvertimeWithScoreDiffrenceTwo()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.True(new Set(player1, player2, 15, 17).IsValid());
        }

        [Fact]
        public void ReturnFalseForSetWithOvertimeWithScoreDiffrenceMoreThanTwo()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.False(new Set(player1, player2, 13, 17).IsValid());
        }

        [Fact]
        public void ReturnFalseForSetWithOvertimeWithScoreDiffrenceLessThanTwo()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.False(new Set(player1, player2, 13, 14).IsValid());
        }

        [Fact]
        public void ReturnFalseForSetWithOvertimeWithNoScoreDifference()
        {
            Player player1 = new Player("1");
            Player player2 = new Player("2");
            Assert.False(new Set(player1, player2, 12, 12).IsValid());
        }
    }
}
