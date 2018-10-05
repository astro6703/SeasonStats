using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeasonStats.Model
{
    public class MatchRepository : IMatchRepository
    {
        private readonly IMongoClient client;

        public MatchRepository(IMongoClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            var db = client.GetDatabase("SeasonStats");

            return await db.GetCollection<Match>("Matches")
                .AsQueryable()
                .ToListAsync();
        }

        public async Task SaveOneAsync(Match match)
        {
            if (match == null) throw new ArgumentNullException(nameof(match));

            var db = client.GetDatabase("SeasonStats");

            await db.GetCollection<Match>("Matches").InsertOneAsync(match);
        }
    }
}