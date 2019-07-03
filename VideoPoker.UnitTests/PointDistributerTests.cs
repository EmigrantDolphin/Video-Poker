using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace VideoPoker.UnitTests {
    [TestClass]
    public class PointDistributerTests {

        [TestMethod]
        public void EvaluateHand_PassingRoyalFlushCards_ReturnRoyalFlush() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Ace, CardType.Hearts),
                Second = new Card(CardRank.Ten, CardType.Hearts),
                Third = new Card(CardRank.King, CardType.Hearts),
                Fourth = new Card(CardRank.Jack, CardType.Hearts),
                Fifth = new Card(CardRank.Queen, CardType.Hearts)
            };


            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.RoyalFlush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingStraightFlushCards_ReturnStraightFlush() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Five, CardType.Clubs),
                Second = new Card(CardRank.Four, CardType.Clubs),
                Third = new Card(CardRank.Seven, CardType.Clubs),
                Fourth = new Card(CardRank.Six, CardType.Clubs),
                Fifth = new Card(CardRank.Eight, CardType.Clubs)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.StraightFlush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFourOfAKindCards_ReturnFourOfAKind() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Five, CardType.Spades),
                Second = new Card(CardRank.Five, CardType.Spades),
                Third = new Card(CardRank.Five, CardType.Spades),
                Fourth = new Card(CardRank.Five, CardType.Spades),
                Fifth = new Card(CardRank.Eight, CardType.Spades)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.FourOfAKind, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFullHouseCards_ReturnFullHouse() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Five, CardType.Hearts),
                Second = new Card(CardRank.Five, CardType.Hearts),
                Third = new Card(CardRank.Eight, CardType.Hearts),
                Fourth = new Card(CardRank.Eight, CardType.Hearts),
                Fifth = new Card(CardRank.Eight, CardType.Hearts)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.FullHouse, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingFlushCards_ReturnFlush() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Five, CardType.Diamonds),
                Second = new Card(CardRank.Five, CardType.Diamonds),
                Third = new Card(CardRank.Three, CardType.Diamonds),
                Fourth = new Card(CardRank.Eight, CardType.Diamonds),
                Fifth = new Card(CardRank.Eight, CardType.Diamonds)
                };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.Flush, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingStraightCards_ReturnStraight() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Jack, CardType.Diamonds),
                Second = new Card(CardRank.Queen, CardType.Diamonds),
                Third = new Card(CardRank.Nine, CardType.Diamonds),
                Fourth = new Card(CardRank.Eight, CardType.Diamonds),
                Fifth = new Card(CardRank.Ten, CardType.Clubs)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.Straight, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingThreeOfAKindCards_ReturnThreeOfAKind() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Queen, CardType.Diamonds),
                Second = new Card(CardRank.Queen, CardType.Diamonds),
                Third = new Card(CardRank.Queen, CardType.Diamonds),
                Fourth = new Card(CardRank.Eight, CardType.Diamonds),
                Fifth = new Card(CardRank.Ten, CardType.Clubs)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.ThreeOfAKind, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingTwoPairCards_ReturnTwoPair() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Queen, CardType.Diamonds),
                Second = new Card(CardRank.Queen, CardType.Diamonds),
                Third = new Card(CardRank.King, CardType.Diamonds),
                Fourth = new Card(CardRank.Eight, CardType.Diamonds),
                Fifth = new Card(CardRank.Ten, CardType.Clubs)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.TwoPair, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingJacksOrBetterCards_ReturnJacksOrBetter() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Queen, CardType.Diamonds),
                Second = new Card(CardRank.Ace, CardType.Diamonds),
                Third = new Card(CardRank.King, CardType.Diamonds),
                Fourth = new Card(CardRank.Eight, CardType.Diamonds),
                Fifth = new Card(CardRank.Ten, CardType.Clubs)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.JacksOrBetter, result.Hand);
        }

        [TestMethod]
        public void EvaluateHand_PassingLoseCards_ReturnLost() {
            CardHand cardHand = new CardHand() {
                First = new Card(CardRank.Three, CardType.Diamonds),
                Second = new Card(CardRank.One, CardType.Diamonds),
                Third = new Card(CardRank.King, CardType.Diamonds),
                Fourth = new Card(CardRank.Eight, CardType.Diamonds),
                Fifth = new Card(CardRank.Ten, CardType.Clubs)
            };

            var result = PointDistributer.EvaluateHand(cardHand);

            Assert.AreEqual(PokerHand.Lost, result.Hand);
        }


    }
}