using SeasonStats.Controllers;
using SeasonStats.Model;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace SeasonStats.Tests
{
    public class PlayerControllerTests
    {
        [Fact]
        public async Task PlayerRepository_SaveOneAsyncGetsCalledWithANameToSave()
        {
            var repository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(repository);
            var name = "Gleb";

            await controller.Create(name);

            await repository.Received().SaveOneAsync(new Player(name));
        }

        [Fact]
        public async Task PlayerRepositiry_GetAllReturnsPlayersFromRepository()
        {
            var repository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(repository);

            var expected = new Player[] { new Player("Gleb"), new Player("Not Gleb") };
            repository.GetAllAsync().Returns(expected);

            var actual = await controller.GetAll();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PlayerController_CreateThrowsArgumentNullException()
        {
            var repository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(repository);

            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.Create(null));
        }

        [Fact]
        public async Task PlayerController_CreateReturnsBadRequestWhenPlayerExists()
        {
            var repository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(repository);

            var player = "Gleb";
            repository.GetOneAsync(player).Returns(new Player("Gleb"));

            Assert.IsType<BadRequestObjectResult>(await controller.Create(player));
        }
    }
}