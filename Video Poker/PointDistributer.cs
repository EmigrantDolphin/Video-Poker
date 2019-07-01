using System.Collections.Generic;


namespace Video_Poker {

    enum PokerHand { RoyalFlush, StraightFlush, FourOfAKind, FullHouse, Flush, Straight, ThreeOfAKind, TwoPair, JacksOrBetter, None }

    struct EvaluateResult {
        public string Message;
        public int Score;
        public PokerHand Hand;

        public EvaluateResult(PokerHand hand, int score, string message) {
            Message = message;
            Score = score;
            Hand = hand;
        }
    }

    class PointDistributer {

        public static EvaluateResult EvaluateHand(List<ICard> cards) {
            List<ICard> cardsCopy = new List<ICard>(cards);
            SortedDictionary<CardRank, int> rankCounter = new SortedDictionary<CardRank, int>(); // how many cards of same rank
            SortByRank(cardsCopy);
            PopulateRankCounter(cardsCopy, rankCounter);

  

            if (IsRoyalFlush(cardsCopy))
                return new EvaluateResult(PokerHand.RoyalFlush, 800, "Royal Flush!");
            if (IsStraightFlush(cardsCopy))
                return new EvaluateResult(PokerHand.StraightFlush, 50, "Straigh Flush!");
            if (IsFourOfAKind(rankCounter))
                return new EvaluateResult(PokerHand.FourOfAKind, 25, "Four Of A Kind!");
            if (IsFullHouse(rankCounter))
                return new EvaluateResult(PokerHand.FullHouse, 9, "Full House!");
            if (IsFlush(cardsCopy))
                return new EvaluateResult(PokerHand.Flush, 6, "Flush!");
            if (IsStraight(cardsCopy))
                return new EvaluateResult(PokerHand.Straight, 4, "Straight!");
            if (IsThreeOfAKind(rankCounter))
                return new EvaluateResult(PokerHand.ThreeOfAKind, 3, "Three Of A Kind!");
            if (IsTwoPair(rankCounter))
                return new EvaluateResult(PokerHand.TwoPair, 2, "Two Pair!");
            if (IsJacksOrBetter(cardsCopy))
                return new EvaluateResult(PokerHand.JacksOrBetter, 1, "Jacks Or Better!");

            return new EvaluateResult(PokerHand.None, 0, "You win NOTHING");
        }


        private static void PopulateRankCounter(List<ICard> cards, SortedDictionary<CardRank, int> rankCounter) {
            rankCounter = new SortedDictionary<CardRank, int>();
            for (int i = 0; i < cards.Count; i++)
                if (!rankCounter.ContainsKey(cards[i].Rank))
                    rankCounter.Add(cards[i].Rank, 0);
            for (int i = 0; i < cards.Count; i++)
                rankCounter[cards[i].Rank]++;
        }

        private static void SortByRank(List<ICard> cards) {
            for (int i = 0; i < cards.Count - 1; i++)
                for (int j = i + 1; j < cards.Count; j++)
                    if (cards[i].Rank > cards[j].Rank) {
                        ICard tempCard = cards[i];
                        cards[i] = cards[j];
                        cards[j] = tempCard;
                    }
        }

        private static bool IsSequential(List<ICard> cards) {
            for (int i = 0; i < cards.Count - 1; i++) {
                if (cards[i].Rank == CardRank.Ace) // ace can only be last card, var i here can't be last card
                    return false;

                if (cards[i].Rank + 1 != cards[i + 1].Rank)
                    return false;
            }
            return true;
        }

        //*********
        //Hand bools below

        private static bool IsRoyalFlush(List<ICard> cards) {
            bool isSameSuit = true;

            for (int i = 0; i < cards.Count - 1; i++)
                if (cards[i].Type != cards[i + 1].Type) {
                    isSameSuit = false;
                    break;
                }

            if (cards[0].Rank == CardRank.Ten && IsSequential(cards) && isSameSuit)
                return true;

            return false;
        }
        private static bool IsStraightFlush(List<ICard> cards) {
            for (int i = 0; i < cards.Count - 1; i++)
                if (cards[i].Type != cards[i + 1].Type)
                    return false;

            if (!IsSequential(cards))
                return false;

            return true;
        }

        private static bool IsFourOfAKind(SortedDictionary<CardRank, int> rankCounter) {
            foreach (KeyValuePair<CardRank, int> pair in rankCounter)
                if (pair.Value == 4)
                    return true;

            return false;
        }

        private static bool IsFullHouse(SortedDictionary<CardRank, int> rankCounter) {
            bool hasTwoSameRanks = false;
            bool hasThreeSameRanks = false;

            foreach (KeyValuePair<CardRank, int> pair in rankCounter) {
                if (pair.Value == 2)
                    hasTwoSameRanks = true;
                if (pair.Value == 3)
                    hasThreeSameRanks = true;
            }

            if (hasTwoSameRanks && hasThreeSameRanks)
                return true;

            return false;
        }

        private static bool IsFlush(List<ICard> cards) {
            for (int i = 0; i < cards.Count - 1; i++)
                if (cards[i].Type != cards[i + 1].Type)
                    return false;

            return true;
        }

        private static bool IsStraight(List<ICard> cards) {
            if (IsSequential(cards))
                return true;
            return false;
        }

        private static bool IsThreeOfAKind(SortedDictionary<CardRank, int> rankCounter) {
            foreach (KeyValuePair<CardRank, int> pair in rankCounter)
                if (pair.Value == 3)
                    return true;
            return false;
        }

        private static bool IsTwoPair(SortedDictionary<CardRank, int> rankCounter) {
            foreach (KeyValuePair<CardRank, int> pair in rankCounter)
                if (pair.Value == 2)
                    return true;
            return false;
        }

        private static bool IsJacksOrBetter(List<ICard> cards) {
            int jacksOrBetterCounter = 0;
            for (int i = 0; i < cards.Count; i++)
                if (cards[i].Rank >= CardRank.Jack)
                    jacksOrBetterCounter++;
            if (jacksOrBetterCounter >= 2)
                return true;
            return false;
        }


    }
}
