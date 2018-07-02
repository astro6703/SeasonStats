using System;

namespace SeasonStats.Model
{
    public class Player
    {
        public string Name { get; }

        public Player(string Name)
        {
            this.Name = Name;
        }

        public bool Equals(Player player)
        {
            return Name == player.Name;
        }
    }
}
