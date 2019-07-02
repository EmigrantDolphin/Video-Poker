using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VideoPoker.UnitTests {
    [TestClass]
    public class CardHandTests {
        [TestMethod]
        public void CardList_IsCount5_Return5() {
            int maxCardsInHand = 5;
            CardHand cardHand = new CardHand();

            var result = cardHand.CardList.Count;

            Assert.AreEqual(result, maxCardsInHand);

        }
    }
}
