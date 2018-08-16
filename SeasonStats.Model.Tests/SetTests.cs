using System;
using Xunit;

namespace SeasonStats.Model.Tests
{
    public class SetTests
    {
        [Fact]
        public void ConstructorThrowsArgumentNullException()
        {
            Player player1 = null;
            var player2 = new Player("2");

            Assert.Throws<ArgumentNullException>(() => new Set(player1, player2, 5, 5));
        }

        [Fact]
        public void ConstructorThrowsArgumentException()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.Throws<ArgumentException>(() => new Set(player1, player2, -5, 5));
        }

        [Fact]
        public void IsValidReturnsTrueForSetWithNoOvertimeAndScoreDifferenceTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.True(new Set(player1, player2, 11, 9).IsValid());
        }
        
        [Fact]
        public void IsValidReturnsTrueForSetWithNoOvertimeAndScoreDifferenceMoreThanTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.True(new Set(player1, player2, 11, 7).IsValid());
        }

        [Fact]
        public void IsValidReturnsFalseForSetWithNoOvertimeAndScoreDifferenceLessThanTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.False(new Set(player1, player2, 11, 10).IsValid());
        }

        [Fact]
        public void IsValidReturnsTrueForSetWithOvertimeWithOneOfScoresTenAndScoreDiffrenceTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.True(new Set(player1, player2, 10, 12).IsValid());
        }

        [Fact]
        public void IsValidReturnsTrueForSetWithOvertimeWithScoreDiffrenceTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.True(new Set(player1, player2, 15, 17).IsValid());
        }

        [Fact]
        public void IsValidReturnsFalseForSetWithOvertimeWithScoreDiffrenceMoreThanTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.False(new Set(player1, player2, 13, 17).IsValid());
        }

        [Fact]
        public void IsValidReturnsFalseForSetWithOvertimeWithScoreDiffrenceLessThanTwo()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.False(new Set(player1, player2, 13, 14).IsValid());
        }

        [Fact]
        public void IsValidReturnsFalseForSetWithOvertimeWithNoScoreDifference()
        {
            var player1 = new Player("1");
            var player2 = new Player("2");

            Assert.False(new Set(player1, player2, 12, 12).IsValid());
        }
    }
}
