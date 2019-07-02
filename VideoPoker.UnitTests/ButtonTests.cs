using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VideoPoker.UnitTests {
    [TestClass]
    public class ButtonTests {

        [TestMethod]
        public void Press_PerformAnAction_ResultActionPerformed() {
            bool actionPerformed = false;

            Button button = new Button(() => actionPerformed = true);
            button.Press();

            Assert.IsTrue(actionPerformed);
        }

    }
}
