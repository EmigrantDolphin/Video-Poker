using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VideoPoker.UnitTests {
    [TestClass]
    public class PointDistributerTests {

        [TestMethod]
        public void EvaluateHand_PassingRoyalFlushCards_ReturnRoyalFlush() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Ace, CardType.Hearts);
            cardHand.Second = new Card(CardRank.Ten, CardType.Hearts);
            cardHand.Third = new Card(CardRank.King, CardType.Hearts);
            cardHand.Fourth = new Card(CardRank.Jack, CardType.Hearts);
            cardHand.Fifth = new Card(CardRank.Queen, CardType.Hearts);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.RoyalFlush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingStraightFlushCards_ReturnStraightFlush() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Five, CardType.Clubs);
            cardHand.Second = new Card(CardRank.Four, CardType.Clubs);
            cardHand.Third = new Card(CardRank.Seven, CardType.Clubs);
            cardHand.Fourth = new Card(CardRank.Six, CardType.Clubs);
            cardHand.Fifth = new Card(CardRank.Eight, CardType.Clubs);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.StraightFlush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFourOfAKindCards_ReturnFourOfAKind() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Five, CardType.Spades);
            cardHand.Second = new Card(CardRank.Five, CardType.Spades);
            cardHand.Third = new Card(CardRank.Five, CardType.Spades);
            cardHand.Fourth = new Card(CardRank.Five, CardType.Spades);
            cardHand.Fifth = new Card(CardRank.Eight, CardType.Spades);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.FourOfAKind, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFullHouseCards_ReturnFullHouse() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Five, CardType.Hearts);
            cardHand.Second = new Card(CardRank.Five, CardType.Hearts);
            cardHand.Third = new Card(CardRank.Eight, CardType.Hearts);
            cardHand.Fourth = new Card(CardRank.Eight, CardType.Hearts);
            cardHand.Fifth = new Card(CardRank.Eight, CardType.Hearts);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.FullHouse, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFlushCards_ReturnFlush() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Five, CardType.Diamonds);
            cardHand.Second = new Card(CardRank.Five, CardType.Diamonds);
            cardHand.Third = new Card(CardRank.Three, CardType.Diamonds);
            cardHand.Fourth = new Card(CardRank.Eight, CardType.Diamonds);
            cardHand.Fifth = new Card(CardRank.Eight, CardType.Diamonds);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.Flush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingStraightCards_ReturnStraight() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Jack, CardType.Diamonds);
            cardHand.Second = new Card(CardRank.Queen, CardType.Diamonds);
            cardHand.Third = new Card(CardRank.Nine, CardType.Diamonds);
            cardHand.Fourth = new Card(CardRank.Eight, CardType.Diamonds);
            cardHand.Fifth = new Card(CardRank.Ten, CardType.Clubs);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.Straight, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingThreeOfAKindCards_ReturnThreeOfAKind() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Queen, CardType.Diamonds);
            cardHand.Second = new Card(CardRank.Queen, CardType.Diamonds);
            cardHand.Third = new Card(CardRank.Queen, CardType.Diamonds);
            cardHand.Fourth = new Card(CardRank.Eight, CardType.Diamonds);
            cardHand.Fifth = new Card(CardRank.Ten, CardType.Clubs);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.ThreeOfAKind, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingTwoPairCards_ReturnTwoPair() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Queen, CardType.Diamonds);
            cardHand.Second = new Card(CardRank.Queen, CardType.Diamonds);
            cardHand.Third = new Card(CardRank.King, CardType.Diamonds);
            cardHand.Fourth = new Card(CardRank.Eight, CardType.Diamonds);
            cardHand.Fifth = new Card(CardRank.Ten, CardType.Clubs);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.TwoPair, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingJacksOrBetterCards_ReturnJacksOrBetter() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Queen, CardType.Diamonds);
            cardHand.Second = new Card(CardRank.Ace, CardType.Diamonds);
            cardHand.Third = new Card(CardRank.King, CardType.Diamonds);
            cardHand.Fourth = new Card(CardRank.Eight, CardType.Diamonds);
            cardHand.Fifth = new Card(CardRank.Ten, CardType.Clubs);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.JacksOrBetter, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingLoseCards_ReturnLost() {
            CardHand cardHand = new CardHand();

            cardHand.First = new Card(CardRank.Three, CardType.Diamonds);
            cardHand.Second = new Card(CardRank.One, CardType.Diamonds);
            cardHand.Third = new Card(CardRank.King, CardType.Diamonds);
            cardHand.Fourth = new Card(CardRank.Eight, CardType.Diamonds);
            cardHand.Fifth = new Card(CardRank.Ten, CardType.Clubs);

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.Lost, result.Hand);
        }


    }
}