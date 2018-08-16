using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeasonStats.Model
{
    public interface IPlayerRepository
    {
        Task<Player> GetOneAsync(string name);

        Task<IEnumerable<Player>> GetAllAsync();

        Task SaveOneAsync(Player player);
    }
}
