using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeasonStats.Model
{
    public interface IMatchRepository
    {
        Task<IEnumerable<Match>> GetAllAsync();

        Task SaveOneAsync(Match match);
    }
}
