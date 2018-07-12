using System;
using System.Collections.Generic;

namespace SeasonStats.Model
{
    public class Player
    {
        public string Name { get; }

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
