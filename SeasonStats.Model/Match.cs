using System;
using System.Collections.Generic;
using System.Text;

namespace SeasonStats.Model
{
    class Match
    {
        public Player player1 { get; }
        public Player player2 { get; }
        private List<Set> games { get; set; }

        public int P1MatchScore
        {
            get
            {
                int counter = 0;

                foreach (Set set in games)
                    if (set.P1SetScore > set.P2SetScore) counter++;

                return counter;
            }
        }

        public void AddSetToMatch(Set set)
        {
            if (!set.player1.Equals(player1) || !set.player2.Equals(player2))
                throw new Exception("Set players doesn't match to match players");
            if (games.Count >= 7) throw new Exception("Number of sets in match has been exceeded");
            games.Add(set);
        }
    }
}
