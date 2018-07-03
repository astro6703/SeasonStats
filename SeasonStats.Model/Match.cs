using System;
using System.Collections.Generic;
using System.Linq;

namespace SeasonStats.Model
{
    public class Match
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        private List<Set> sets;

        public int Player1Score => sets.Count(set => set.Player1Score > set.Player2Score);
        public int Player2Score => sets.Count(set => set.Player2Score > set.Player1Score);

        public void AddSet(Set set)
        {
            if (!set.Player1.Equals(Player1) || !set.Player2.Equals(Player2)) throw new Exception("Set players doesn't match to match players");
            if (sets.Count >= 7) throw new Exception("Number of sets in match has been exceeded");
            if (set == null) throw new ArgumentNullException();

            sets.Add(set);
        }
    }
}
