using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VideoPoker.UnitTests {
    [TestClass]
    public class PointDistributerTests {

        [TestMethod]
        public void EvaluateHand_PassingRoyalFlushCards_ReturnRoyalFlush() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Ace, CardType.Hearts));
            cards.Add(new Card(CardRank.Ten, CardType.Hearts));
            cards.Add(new Card(CardRank.King, CardType.Hearts));
            cards.Add(new Card(CardRank.Jack, CardType.Hearts));
            cards.Add(new Card(CardRank.Queen, CardType.Hearts));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.RoyalFlush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingStraightFlushCards_ReturnStraightFlush() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Five, CardType.Clubs));
            cards.Add(new Card(CardRank.Four, CardType.Clubs));
            cards.Add(new Card(CardRank.Seven, CardType.Clubs));
            cards.Add(new Card(CardRank.Six, CardType.Clubs));
            cards.Add(new Card(CardRank.Eight, CardType.Clubs));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.StraightFlush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFourOfAKindCards_ReturnFourOfAKind() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Five, CardType.Spades));
            cards.Add(new Card(CardRank.Five, CardType.Spades));
            cards.Add(new Card(CardRank.Five, CardType.Spades));
            cards.Add(new Card(CardRank.Five, CardType.Spades));
            cards.Add(new Card(CardRank.Eight, CardType.Spades));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.FourOfAKind, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFullHouseCards_ReturnFullHouse() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Five, CardType.Hearts));
            cards.Add(new Card(CardRank.Five, CardType.Hearts));
            cards.Add(new Card(CardRank.Eight, CardType.Hearts));
            cards.Add(new Card(CardRank.Eight, CardType.Hearts));
            cards.Add(new Card(CardRank.Eight, CardType.Hearts));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.FullHouse, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFlushCards_ReturnFlush() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Five, CardType.Diamonds));
            cards.Add(new Card(CardRank.Five, CardType.Diamonds));
            cards.Add(new Card(CardRank.Three, CardType.Diamonds));
            cards.Add(new Card(CardRank.Eight, CardType.Diamonds));
            cards.Add(new Card(CardRank.Eight, CardType.Diamonds));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.Flush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingStraightCards_ReturnStraight() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Jack, CardType.Diamonds));
            cards.Add(new Card(CardRank.Queen, CardType.Diamonds));
            cards.Add(new Card(CardRank.Nine, CardType.Diamonds));
            cards.Add(new Card(CardRank.Eight, CardType.Diamonds));
            cards.Add(new Card(CardRank.Ten, CardType.Clubs));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.Straight, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingThreeOfAKindCards_ReturnThreeOfAKind() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Queen, CardType.Diamonds));
            cards.Add(new Card(CardRank.Queen, CardType.Diamonds));
            cards.Add(new Card(CardRank.Queen, CardType.Diamonds));
            cards.Add(new Card(CardRank.Eight, CardType.Diamonds));
            cards.Add(new Card(CardRank.Ten, CardType.Clubs));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.ThreeOfAKind, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingTwoPairCards_ReturnTwoPair() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Queen, CardType.Diamonds));
            cards.Add(new Card(CardRank.Queen, CardType.Diamonds));
            cards.Add(new Card(CardRank.King, CardType.Diamonds));
            cards.Add(new Card(CardRank.Eight, CardType.Diamonds));
            cards.Add(new Card(CardRank.Ten, CardType.Clubs));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.TwoPair, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingJacksOrBetterCards_ReturnJacksOrBetter() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Queen, CardType.Diamonds));
            cards.Add(new Card(CardRank.Ace, CardType.Diamonds));
            cards.Add(new Card(CardRank.King, CardType.Diamonds));
            cards.Add(new Card(CardRank.Eight, CardType.Diamonds));
            cards.Add(new Card(CardRank.Ten, CardType.Clubs));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.JacksOrBetter, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingLoseCards_ReturnLost() {
            List<ICard> cards = new List<ICard>();

            cards.Add(new Card(CardRank.Three, CardType.Diamonds));
            cards.Add(new Card(CardRank.One, CardType.Diamonds));
            cards.Add(new Card(CardRank.King, CardType.Diamonds));
            cards.Add(new Card(CardRank.Eight, CardType.Diamonds));
            cards.Add(new Card(CardRank.Ten, CardType.Clubs));

            var result = PointDistributer.EvaluateHand(cards);

            Assert.AreEqual(PokerHand.Lost, result.Hand);
        }


    }
}