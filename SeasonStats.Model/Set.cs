using System;
using System.Collections.Generic;
using System.Text;

namespace SeasonStats.Model
{
    class Set
    {
        public Player player1 { get; }
        public Player player2 { get; }
        public int P1SetScore { get; }
        public int P2SetScore { get; }

        public Set(Player player1, Player player2, int P1SetScore, int P2SetScore)
        {
            if (P1SetScore >= 0 && P1SetScore <= 11 && P2SetScore >= 0 && P2SetScore <= 11)
            {
                this.P1SetScore = P1SetScore;
                this.P2SetScore = P2SetScore;
            }
            else throw new Exception("Invalid score");
        }
    }
}
