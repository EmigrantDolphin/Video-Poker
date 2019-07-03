using System;
using System.Collections.Generic;

namespace VideoPoker {
    class GameScene : IDrawable {
        enum DrawState { FirstHand, DrawnHand }
        List<IFocusable> focusables;
        List<IDrawable> drawables;

        // currently focused item index in focusable list
        int focusIndex = 0; 

        CardHand cardHand;

        int score = 0;
        Text scoreText = new Text(new Vector2(40, 1), "Score: 0");
        readonly Text messageForDiscard = new Text(new Vector2(34, 23), "Selected cards to discard");
        readonly Text messageForDraw = new Text(new Vector2(34, 23), "Draw to gamble again");
        Text messageForResult = new Text(new Vector2(3, 30));

        Button drawButton;
        string drawButtonMessage = "Draw";

        DrawState drawState = DrawState.FirstHand;

        public GameScene() {

            //Initializing variables
            focusables = new List<IFocusable>();
            drawables = new List<IDrawable>();
            cardHand = new CardHand() {
                Position = new Vector2(3, 3)
            };

            foreach (ICard card in cardHand) {
                focusables.Add(card as IFocusable);
                drawables.Add(card as IDrawable);
            }
            drawables.Add(scoreText);

            InitButtons();

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
                    scoreText.Message = "Score: " + score;
                    messageForResult.Message = evaluateResult.Message + ", you win " + evaluateResult.Score + " points";
                    messageForResult.Draw();
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
            drawButton.Position = new Vector2(cardHand.Position.x, cardHand.Position.y + cardHand.Height + drawButton.Height / 2);
            drawButton.Message = drawButtonMessage;

            drawables.Add(drawButton as IDrawable);
            focusables.Add(drawButton as IFocusable);
        }

        public void Draw() {
            if (drawState == DrawState.FirstHand)
                messageForDiscard.Draw();
            else
                messageForDraw.Draw();

            //draw drawables
            foreach (IDrawable drawable in drawables)
                drawable.Draw();
        }

        public void MoveFocusUp() {
            focusables[focusIndex].Focus(false);
            if (focusIndex < focusables.Count - 1)
                focusIndex++;
            focusables[focusIndex].Focus(true);
        }

        public void MoveFocusDown() {
            focusables[focusIndex].Focus(false);
            if (focusIndex > 0)
                focusIndex--;
            focusables[focusIndex].Focus(true);
        }

        public void PressSelected() {

            // onFirsHand everything can be selected/pressed
            // on drawnHand cards can't be selected/pressed
            if (drawState == DrawState.FirstHand) 
                focusables[focusIndex].Press();
            else if (!(focusables[focusIndex] is ICard)) 
                focusables[focusIndex].Press();          
        }
    }
}
