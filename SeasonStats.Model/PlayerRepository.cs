using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace SeasonStats.Model
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IMongoClient client;    

        public PlayerRepository(IMongoClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Player> GetOneAsync(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var db = client.GetDatabase("SeasonStats");

            var collection = db.GetCollection<Player>("Players")
                .AsQueryable();

            return await collection
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            var db = client.GetDatabase("SeasonStats");

            return await db.GetCollection<Player>("Players")
                .AsQueryable()
                .ToListAsync();
        }

        public async Task SaveOneAsync(Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            var db = client.GetDatabase("SeasonStats");

            player.Id = player.Id ?? ObjectId.GenerateNewId();

            var filter = new BsonDocument("_id", player.Id);

            await db.GetCollection<Player>("Players")
                .ReplaceOneAsync(filter, player, new UpdateOptions { IsUpsert = true });
        }
    }
}