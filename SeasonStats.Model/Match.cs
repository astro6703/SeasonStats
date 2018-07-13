using System;
using System.Collections.Generic;
using System.Linq;

namespace SeasonStats.Model
{
    public class Match
    {
        public Player Player1 => sets.FirstOrDefault()?.Player1;
        public Player Player2 => sets.FirstOrDefault()?.Player2;

        public int MaximalNumberOfSets { get; }
        private List<Set> sets = new List<Set>();

        public Match(int MaxSets)
        {
            if (MaxSets != 1 && MaxSets != 3 && MaxSets != 5 && MaxSets != 7) throw new ArgumentException("Wrong maximal number of sets");

            MaximalNumberOfSets = MaxSets;
        }

        public int Player1Score => sets.Count(set => set.Player1Score > set.Player2Score);
        public int Player2Score => sets.Count(set => set.Player2Score > set.Player1Score);

        public void AddSet(Set set)
        {
            if (set == null) throw new ArgumentNullException();
            if (!set.IsValid()) throw new InvalidOperationException("Given set is not valid");
            if (!ArePlayersEqual(set)) throw new InvalidOperationException("Match players doesn't match to match players");
            if (IsMatchFinished()) throw new InvalidOperationException("The match has already been finished");

            sets.Add(set);
        }

        private bool IsMatchFinished()
        {
            return ((Player1Score == (MaximalNumberOfSets + 1) / 2) || Player2Score == (MaximalNumberOfSets + 1) / 2);
        }

        private bool ArePlayersEqual(Set set)
        {
            return !sets.Any() || set.Player1.Equals(Player1) && set.Player2.Equals(Player2);
        }
    }
}
