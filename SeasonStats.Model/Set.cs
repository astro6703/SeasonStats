using System;
using System.Collections.Generic;
using System.Text;

namespace SeasonStats.Model
{
    public class Set
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        public int Player1Score { get; }
        public int Player2Score { get; }

        public Set(Player player1, Player player2, int player1Score, int player2Score)
        {
            if (player1Score < 0 || player2Score < 0) throw new ArgumentException("Invalid score");
            if (player1 == null || player2 == null) throw new ArgumentNullException();

            Player1 = player1;
            Player2 = player2;
            Player1Score = player1Score;
            Player2Score = player2Score;
        }

        public bool IsValid()
        {
            if (((Player1Score == 11 ^ Player2Score == 11) && Math.Abs(Player1Score - Player2Score) >= 2) ||
                (Player1Score >= 10 && Player2Score >= 10 && Math.Abs(Player1Score - Player2Score) == 2))
                return true;
            else return false;
        }
    }
}
