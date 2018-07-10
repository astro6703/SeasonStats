using System;
using System.Collections.Generic;
using System.Linq;

namespace SeasonStats.Model
{
    public class Match
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int MaximalNumberOfSets { get; }
        private List<Set> sets = new List<Set>();

        public Match(int MaxSets)
        {
            if (MaxSets != 3 && MaxSets != 5 && MaxSets != 7) throw new ArgumentException();

            MaximalNumberOfSets = MaxSets;
        }

        public int Player1Score => sets.Count(set => set.Player1Score > set.Player2Score);
        public int Player2Score => sets.Count(set => set.Player2Score > set.Player1Score);

        public void AddSet(Set set)
        {
            if (set == null) throw new ArgumentNullException();
            if (!set.IsValid()) throw new Exception("Given set is not valid");
            if (sets.Count == 0)
            {
                Player1 = set.Player1;
                Player2 = set.Player2;
            }
            if (!set.Player1.Equals(Player1) || !set.Player2.Equals(Player2)) throw new Exception("Match players doesn't match to match players");
            if ((Player1Score == (MaximalNumberOfSets + 1) / 2) || Player1Score == (MaximalNumberOfSets + 1) / 2)
                throw new Exception("The match has already got a winner");

            sets.Add(set);
        }
    }
}
