using SeasonStats.Controllers;
using SeasonStats.Model;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;

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
        public async Task PlayerRepositiryGetAllGetsCalled()
        {
            IPlayerRepository playerRepository = Substitute.For<IPlayerRepository>();
            var controller = new PlayerController(playerRepository);

            await controller.GetAll();

            await playerRepository.Received().GetAllAsync();
        }
    }
}
