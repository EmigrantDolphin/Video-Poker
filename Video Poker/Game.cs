using System;
using System.Collections.Generic;

namespace VideoPoker {
    public enum CardType { Hearts, Clubs, Spades, Diamonds }
    public enum CardRank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    class Game {
        GameScene gameScene;
        public Game() {
            gameScene = new GameScene();
        }

        public void Loop() {
            while (true) {
                Console.SetCursorPosition(0, 0);
                ConsoleKeyInfo keyInfo = Console.ReadKey(); 

                if (keyInfo.Key == ConsoleKey.RightArrow)
                    gameScene.MoveFocusUp();
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                    gameScene.MoveFocusDown();
                if (keyInfo.Key == ConsoleKey.Enter) {
                    gameScene.PressSelected();
                }

                gameScene.Draw();
                if (keyInfo.Key == ConsoleKey.Escape)
                    break;
            }
        }

    }
}
