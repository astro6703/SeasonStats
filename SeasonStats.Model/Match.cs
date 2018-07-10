using System;
using System.Collections.Generic;
using System.Linq;

namespace SeasonStats.Model
{
    public class Match
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private List<Set> sets = new List<Set>();

        public int Player1Score => sets.Count(set => set.Player1Score > set.Player2Score);
        public int Player2Score => sets.Count(set => set.Player2Score > set.Player1Score);

        public void AddSet(Set set)
        {
            if (set == null) throw new ArgumentNullException();
            if (!set.IsValid()) throw new Exception("Given set is not valid");
            if (sets.Count == 7) throw new Exception("Number of sets in match has been exceeded");
            if (sets.Count == 0)
            {
                Player1 = set.Player1;
                Player2 = set.Player2;
            }
            if (!set.Player1.Equals(Player1) || !set.Player2.Equals(Player2)) throw new Exception("Match players doesn't match to match players");

            sets.Add(set);
        }
    }
}
