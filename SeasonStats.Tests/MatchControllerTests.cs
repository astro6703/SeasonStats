using System;
using SeasonStats.Model;
using SeasonStats.Controllers;
using Xunit;
using NSubstitute;
using System.Threading.Tasks;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

namespace SeasonStats.Tests
{
    public class MatchControllerTests
    {
        [Fact]
        public async Task CreateMatch_ThrowsExceptionWhenFirstArgumentIsNull()
        {
            var matchRepository = Substitute.For<IMatchRepository>();
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            var scores = new int[] { 11, 5 };
            var player2 = "Gleb";

            await Assert.ThrowsAsync<ArgumentNullException>(async () 
                => await matchController.Create(null, player2, scores));
        }

        [Fact]
        public async Task CreateMatch_ThrowsExceptionWhenSecondArgumentIsNull()
        {
            var matchRepository = Substitute.For<IMatchRepository>();
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            var scores = new int[] { 11, 5 };
            var player1 = "Gleb";

            await Assert.ThrowsAsync<ArgumentNullException>(async () 
                => await matchController.Create(player1, null, scores));
        }

        [Fact]
        public async Task CreateMatch_ReturnsNotFoundWhenDidntFindFirstPlayerInDatabase()
        {
            var matchRepository = Substitute.For<IMatchRepository>();
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            var notExistingPlayer = "sdfnsjdnf";
            var existingPlayer = "Gleb";

            playerRepository.GetOneAsync(notExistingPlayer).Returns((Player) null);
            playerRepository.GetOneAsync(existingPlayer).Returns(new Player("Gleb") { Id = BsonObjectId.Create("5b72be99acf22118c84cf591") });

            var scores = new int[] { 11, 5 };

            Assert.IsType<NotFoundResult>(await matchController.Create(notExistingPlayer, existingPlayer, scores));
        }

        [Fact]
        public async Task CreateMatch_ReturnsNotFoundWhenDidntFindSecondPlayerInDatabase()
        {
            var matchRepository = Substitute.For<IMatchRepository>();
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            var notExistingPlayer = "sdfnsjdnf";
            var existingPlayer = "Gleb";

            playerRepository.GetOneAsync(notExistingPlayer).Returns((Player)null);
            playerRepository.GetOneAsync(existingPlayer).Returns(new Player("Gleb") { Id = BsonObjectId.Create("5b72be99acf22118c84cf591") });

            var scores = new int[] { 11, 5 };

            Assert.IsType<NotFoundResult>(await matchController.Create(existingPlayer, notExistingPlayer, scores));
        }

        [Fact]
        public async Task GetAll_ReturnsMatchesFromRepository()
        {
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchRepository = Substitute.For<IMatchRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            var match1 = new Match(1);
            match1.AddSet(new Set(new Player("Gleb"), new Player("Not Gleb"), 11, 5));
            var expected = new Match[] { match1 };
            matchController.GetAll().Returns(expected);

            var actual = await matchController.GetAll();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CreateMatch_LooksUpPlayersAndSavesBestOf1Match()
        {
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchRepository = Substitute.For<IMatchRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            playerRepository.GetOneAsync("Gleb").Returns(new Player("Gleb") { Id = BsonObjectId.Create("5b72be99acf22118c84cf591") });
            playerRepository.GetOneAsync("Kadetsky").Returns(new Player("Kadetsky") { Id = BsonObjectId.Create("5b51c67b19429c0bf038f4fd") });

            var scores = new int[] { 11, 5 };

            await matchController.Create("Gleb", "Kadetsky", scores);

            var player1 = await playerRepository.GetOneAsync("Gleb");
            var player2 = await playerRepository.GetOneAsync("Kadetsky");

            var match = new Match(1);
            match.AddSet(new Set(player1, player2, scores[0], scores[1]));

            await matchRepository.Received().SaveOneAsync(match);
        }

        [Fact]
        public async Task CreateMatch_LooksUpPlayersAndSavesBestOf3Match()
        {
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchRepository = Substitute.For<IMatchRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            playerRepository.GetOneAsync("Gleb").Returns(new Player("Gleb") { Id = BsonObjectId.Create("5b72be99acf22118c84cf591") });
            playerRepository.GetOneAsync("Kadetsky").Returns(new Player("Kadetsky") { Id = BsonObjectId.Create("5b51c67b19429c0bf038f4fd") });

            var scores = new int[] { 11, 5, 5, 11, 11, 5 };

            await matchController.Create("Gleb", "Kadetsky", scores);

            var player1 = await playerRepository.GetOneAsync("Gleb");
            var player2 = await playerRepository.GetOneAsync("Kadetsky");

            var match = new Match(3);
            match.AddSet(new Set(player1, player2, scores[0], scores[1]));
            match.AddSet(new Set(player1, player2, scores[2], scores[3]));
            match.AddSet(new Set(player1, player2, scores[4], scores[5]));

            await matchRepository.Received().SaveOneAsync(match);
        }

        [Fact]
        public async Task CreateMatch_LooksUpPlayersAndSavesBestOf7Match()
        {
            var playerRepository = Substitute.For<IPlayerRepository>();
            var matchRepository = Substitute.For<IMatchRepository>();
            var matchController = new MatchController(matchRepository, playerRepository);

            playerRepository.GetOneAsync("Gleb").Returns(new Player("Gleb") { Id = BsonObjectId.Create("5b72be99acf22118c84cf591") });
            playerRepository.GetOneAsync("Kadetsky").Returns(new Player("Kadetsky") { Id = BsonObjectId.Create("5b51c67b19429c0bf038f4fd") });

            var scores = new int[] { 11, 5, 11, 5, 11, 5, 5, 11, 5, 11, 5, 11, 11, 5 };

            await matchController.Create("Gleb", "Kadetsky", scores);

            var player1 = await playerRepository.GetOneAsync("Gleb");
            var player2 = await playerRepository.GetOneAsync("Kadetsky");

            var match = new Match(7);
            match.AddSet(new Set(player1, player2, scores[0], scores[1]));
            match.AddSet(new Set(player1, player2, scores[2], scores[3]));
            match.AddSet(new Set(player1, player2, scores[4], scores[5]));
            match.AddSet(new Set(player1, player2, scores[6], scores[7]));
            match.AddSet(new Set(player1, player2, scores[8], scores[9]));
            match.AddSet(new Set(player1, player2, scores[10], scores[11]));
            match.AddSet(new Set(player1, player2, scores[12], scores[13]));

            await matchRepository.Received().SaveOneAsync(match);
        }
    }
}