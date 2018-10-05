using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace SeasonStats.Model
{
    public class Player
    {
        [BsonId]
        [JsonIgnore]
        public BsonObjectId Id { get; set; }

        public string Name { get; set; }

        public Player(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override bool Equals(object obj)
        {
            var otherPlayer = obj as Player;

            if (otherPlayer == null) return false;
            return Name == otherPlayer.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
