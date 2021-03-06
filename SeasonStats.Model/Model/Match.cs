﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SeasonStats.Model
{
    public class Match
    {
        [BsonId]
        public BsonObjectId ObjectId { get; set; }
        public int MaximalNumberOfSets { get; set; }
        public List<Set> Sets { get; set; } = new List<Set>();

        public Player Player1 => Sets.FirstOrDefault()?.Player1;

        public Player Player2 => Sets.FirstOrDefault()?.Player2;

        public int Player1Score => Sets.Count(set => set.Player1Score > set.Player2Score);

        public int Player2Score => Sets.Count(set => set.Player2Score > set.Player1Score);

        public Match(int maxSets)
        {
            if (maxSets != 1 && maxSets != 3 && maxSets != 5 && maxSets != 7) throw new ArgumentException("Wrong maximal number of sets");

            MaximalNumberOfSets = maxSets;
        }

        public void AddSet(Set set)
        {
            if (set == null) throw new ArgumentNullException();
            if (!set.IsValid()) throw new InvalidOperationException("Given set is not valid");
            if (!ArePlayersEqual(set)) throw new InvalidOperationException("Match players doesn't match to match players");
            if (IsFinished()) throw new InvalidOperationException("The match has already been finished");

            Sets.Add(set);
        }

        public bool IsFinished()
        {
            return ((Player1Score == (MaximalNumberOfSets + 1) / 2) || Player2Score == (MaximalNumberOfSets + 1) / 2);
        }

        private bool ArePlayersEqual(Set set)
        {
            return !Sets.Any() || set.Player1.Equals(Player1) && set.Player2.Equals(Player2);
        }

        public override bool Equals(object obj)
        {
            var match = obj as Match;

            if (match == null) return false;

            if (match.Sets.Count != Sets.Count || match.MaximalNumberOfSets != MaximalNumberOfSets) return false;

            return Sets.SequenceEqual(match.Sets);
        }

        public override int GetHashCode()
        {
            var hashCode = -412360621;
            hashCode = hashCode * -1521134295 + MaximalNumberOfSets.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Set>>.Default.GetHashCode(Sets);
            return hashCode;
        }
    }
}
