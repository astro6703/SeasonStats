using SeasonStats.Controllers;
using SeasonStats.Model;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using System;

namespace SeasonStats.Tests
{
    public class PlayerControllerTests
    {
        [Fact]
        public async Task PlayerRepositoryGetsCalledWithANameToSave()
        {
            var playerRepository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(playerRepository);
            var name = "Gleb";

            await controller.SaveOne(name);

            await playerRepository.Received().SaveOneAsync(new Player(name));
        }

        [Fact]
        public async Task PlayerRepositiryGetAllReturnsPlayersFromRepository()
        {
            var playerRepository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(playerRepository);

            var expected = new Player[] { new Player("Gleb"), new Player("Not Gleb") };
            playerRepository.GetAllAsync().Returns(expected);

            var actual = await controller.GetAll();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PlayerController_SaveOneAsyncThrowsArgumentNullException()
        {
            var repository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(repository);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await controller.SaveOne(null));
        }
    }
}