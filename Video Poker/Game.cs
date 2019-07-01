using System;
using System.Collections.Generic;

namespace VideoPoker {
    public enum CardType { Hearts, Clubs, Spades, Diamonds }
    public enum CardRank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    class Game {
        enum DrawState { FirstHand, DrawnHand }

        CardHand cardHand;
        ConsoleKeyInfo keyInfo;

        int score = 0;
        Vector2 scorePos = new Vector2(40, 1);
        Text messageForDiscard = new Text("Selected cards to discard");
        Text messageForDraw = new Text("Draw to gamble again");

        List<IFocusable> focusables;
        List<IDrawable> drawables;
        int focusIndex = 0; // currently focused item index in focusable list

        Button drawButton;
        string drawButtonMessage = "Draw";
        DrawState drawState = DrawState.FirstHand;
        int yOffset = 5; // used as distance from cardLayout bottom to button/message top
        int xOffset = 3; // used as distance between buttons

        public Game() {
            //Initializing variables
            focusables = new List<IFocusable>();
            drawables = new List<IDrawable>();
            cardHand = new CardHand();
            cardHand.Position = new Vector2(3, 3);

            foreach (ICard card in cardHand) {
                focusables.Add(card as IFocusable);
                drawables.Add(card as IDrawable);
            }

            InitButtons();

            //setting messagePos that is relative to cardLayout which size is set on population in RandomizeCards()
            Vector2 messagePos = new Vector2(cardHand.Position.x + cardHand.Width / 2 - messageForDiscard.Message.Length / 2,
                                      cardHand.Position.y + cardHand.Height + yOffset
                                    );
            
            messageForDiscard.Position = messagePos;
            messageForDraw.Position = messagePos;

            focusables[0].Focus(true);
            Draw();
        }

        private void InitButtons() {
            //passing lambda expression to action inside button
            drawButton = new Button(() => {
                if (drawState == DrawState.FirstHand) {
                    cardHand.RandomizeSelectedCards();
                    Console.Clear();
                    EvaluateResult evaluateResult = PointDistributer.EvaluateHand(cardHand);
                    score += evaluateResult.Score;
                    Console.SetCursorPosition(3, 30);
                    Console.Write(evaluateResult.Message + ", you win " + evaluateResult.Score + " points");
                } else {
                    cardHand.RandomizeCards();
                    Console.Clear();
                }

                if (drawState == DrawState.FirstHand)
                    drawState = DrawState.DrawnHand;
                else
                    drawState = DrawState.FirstHand;
            });
            //setting position, message and adding to lists
            drawButton.Position = new Vector2(xOffset, cardHand.Position.y + cardHand.Height + yOffset);
            drawButton.Message = drawButtonMessage;

            drawables.Add(drawButton as IDrawable);
            focusables.Add(drawButton as IFocusable);
        }


        public void Loop() {
            while (true) {
                Console.SetCursorPosition(0, 0);
                keyInfo = Console.ReadKey(); //Pause and readKey

                if (keyInfo.Key == ConsoleKey.RightArrow)
                    MoveFocusUp();
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                    MoveFocusDown();
                if (keyInfo.Key == ConsoleKey.Enter) {
                    if (drawState == DrawState.FirstHand) // onFirsHand everything can be selected/pressed
                        focusables[focusIndex].Press();
                    else if (!(focusables[focusIndex] is ICard)) // on drawnHand cards can't be selected/pressed
                        focusables[focusIndex].Press();
                }

                Draw();

                if (keyInfo.Key == ConsoleKey.Escape) // ends game
                    break;
            }
        }

        private void Draw() {
            //draw score and message
            Console.SetCursorPosition(scorePos.x, scorePos.y);
            Console.Write("Score: " + score);

            if (drawState == DrawState.FirstHand)
                messageForDiscard.Draw();
            else
                messageForDraw.Draw();
            //draw drawables
            foreach (IDrawable drawable in drawables)
                drawable.Draw();
        }

        private void MoveFocusUp() {
            focusables[focusIndex].Focus(false);
            if (focusIndex < focusables.Count - 1)
                focusIndex++;
            focusables[focusIndex].Focus(true);
        }

        private void MoveFocusDown() {
            focusables[focusIndex].Focus(false);
            if (focusIndex > 0)
                focusIndex--;
            focusables[focusIndex].Focus(true);
        }


    }
}
