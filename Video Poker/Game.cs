using System;
using System.Collections.Generic;

namespace VideoPoker {
    public enum CardType { Hearts, Clubs, Spades, Diamonds }
    public enum CardRank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    class Game {
        enum DrawState { FirstHand, DrawnHand }

        Deck deck;
        ConsoleKeyInfo keyInfo;

        int score = 0;
        Vector2 scorePos = new Vector2(40, 1);
        string messageForDiscard = "Select cards to discard";
        string messageForDraw = "Draw to gamble again";
        Vector2 messagePos; // assigned relative to CardLayout dimensions;



        CardLayout cardLayout;
        Vector2 layoutPos = new Vector2(3, 3);
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
            cardLayout = new CardLayout();
            cardLayout.Position = layoutPos;

            //populating cardLayout (5 slots) and initializing button position and action etc
            RandomizeCards();
            InitButtons();

            //setting messagePos that is relative to cardLayout which size is set on population in RandomizeCards()
            messagePos = new Vector2(cardLayout.Position.x + cardLayout.Width / 2 - messageForDiscard.Length / 2,
                                      cardLayout.Position.y + cardLayout.Height + yOffset
                                    );
            //
            drawables.Add(cardLayout as IDrawable);
            focusables[0].Focus(true);
            Draw();
        }

        private void InitButtons() {
            //passing lambda expression to action inside button
            drawButton = new Button(() => {
                if (drawState == DrawState.FirstHand) {
                    RandomizeSelectedCards();
                    Console.Clear();
                    EvaluateResult evaluateResult = PointDistributer.EvaluateHand(cardLayout.Cards);
                    score += evaluateResult.Score;
                    Console.SetCursorPosition(3, 30);
                    Console.Write(evaluateResult.Message + ", you win " + evaluateResult.Score + " points");
                } else {
                    RandomizeCards();
                    Console.Clear();
                }

                if (drawState == DrawState.FirstHand)
                    drawState = DrawState.DrawnHand;
                else
                    drawState = DrawState.FirstHand;
            });
            //setting position, message and adding to lists
            drawButton.Position = new Vector2(xOffset, cardLayout.Position.y + cardLayout.Height + yOffset);
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
            Console.SetCursorPosition(messagePos.x, messagePos.y);
            if (drawState == DrawState.FirstHand)
                Console.Write(messageForDiscard);
            else
                Console.Write(messageForDraw);
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

        private void RandomizeSelectedCards() {
            for (int i = 0; i < cardLayout.Count; i++)
                if ((cardLayout.GetCardAt(i) as ISelectable).IsSelected) {
                    int tempFocusIndex = focusables.IndexOf(cardLayout.GetCardAt(i) as IFocusable); // so the order in focusables doesn't change
                    cardLayout.SetCardAt(i, deck.GetRandomCard());
                    focusables[tempFocusIndex] = (cardLayout.GetCardAt(i)) as IFocusable;
                }
        }

        private void RandomizeCards() {
            deck = new Deck();
            if (cardLayout.Count != cardLayout.SlotCount) // runs only once, at the start
                for (int i = 0; i < cardLayout.SlotCount; i++) {
                    cardLayout.AddCard(deck.GetRandomCard());
                    focusables.Add((cardLayout.GetCardAt(i)) as IFocusable);
                } else
                for (int i = 0; i < cardLayout.Count; i++) {
                    int tempFocusIndex = focusables.IndexOf(cardLayout.GetCardAt(i) as IFocusable); // so the order in focusables doesn't change				
                    cardLayout.SetCardAt(i, deck.GetRandomCard());
                    focusables[tempFocusIndex] = (cardLayout.GetCardAt(i)) as IFocusable;
                }
        }


    }
}
