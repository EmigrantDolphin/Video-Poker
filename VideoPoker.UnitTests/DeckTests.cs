using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VideoPoker.UnitTests {
    [TestClass]
    public class DeckTests {
        [TestMethod]
        public void GetRandomCard_ReturnTypeICard() {
            Deck deck = new Deck();

            var result = deck.GetRandomCard();

            Assert.IsInstanceOfType(result, typeof(ICard));
        }
    }
}
