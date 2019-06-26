using System;
using System.Collections.Generic;

enum CardType {Hearts, Clubs, Spades, Diamonds}
enum CardRank {One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace}
enum DrawState {FirstHand, DrawnHand}

class Game{
	Deck deck;
	ConsoleKeyInfo keyInfo;
	
	int score = 0;
	Vector2i scorePos = new Vector2i(40, 1);
	string messageForDiscard = "Select cards to discard";
	string messageForDraw = "Draw to gamble again";
	Vector2i messagePos; // assigned relative to CardLayout dimensions;
	

	
	CardLayout cardLayout;
	Vector2i layoutPos = new Vector2i(3, 3);
	List<IFocusable> focusables;
	List<IDrawable> drawables;
	int focusIndex = 0; // currently focused item index in focusable list
	
	Button drawButton;
	string drawButtonMessage = "Draw";
	DrawState drawState = DrawState.FirstHand;
	int yOffset = 5; // used as distance from cardLayout bottom to button top
	int xOffset = 3; // used as distance between buttons
	
	public Game(){	
		focusables = new List<IFocusable>();
		drawables = new List<IDrawable>();
		cardLayout = new CardLayout();
		cardLayout.Position = layoutPos;
		
		RandomizeCards();
		InitButtons();
		
		drawables.Add(cardLayout as IDrawable);
		
		
		
		messagePos = new Vector2i(cardLayout.Position.x + cardLayout.Width/2 - messageForDiscard.Length/2, 
								  cardLayout.Position.y + cardLayout.Height + yOffset	
								);
		
		
		focusables[0].Focus(true);
		Draw();
	}
	
	private void InitButtons(){
		drawButton = new Button(()=>{
			if (drawState == DrawState.FirstHand){
				RandomizeSelectedCards();
				Console.Clear();
				KeyValuePair<string,int> evaluationResult = PointDistributer.EvaluateHand(cardLayout.Cards);
				score += evaluationResult.Value;
				Console.SetCursorPosition(3, 30);
				Console.Write(evaluationResult.Key + ", you win "+ evaluationResult.Value + " points");
			}else{
				RandomizeCards();
				Console.Clear();
			}
			
			if (drawState == DrawState.FirstHand)
				drawState = DrawState.DrawnHand;
			else
				drawState = DrawState.FirstHand;
		});
		drawButton.Position = new Vector2i(xOffset, cardLayout.Position.y + cardLayout.Height + yOffset);
		drawButton.Message = drawButtonMessage;
		
		drawables.Add(drawButton as IDrawable);
		focusables.Add(drawButton as IFocusable);
	}
	
	
	public void Loop(){
		while (true){
			Console.SetCursorPosition(0,0);
			keyInfo = Console.ReadKey();
			
			if (keyInfo.Key == ConsoleKey.W)
				drawButton.Press();
			
			if (keyInfo.Key == ConsoleKey.RightArrow)
				MoveFocusUp();
			if (keyInfo.Key == ConsoleKey.LeftArrow)
				MoveFocusDown();
			if (keyInfo.Key == ConsoleKey.Enter){
				if (drawState == DrawState.FirstHand)
					focusables[focusIndex].Press();
				else if ( !(focusables[focusIndex] is ICard) )
					focusables[focusIndex].Press();
			}
			
			Draw();
			
			if (keyInfo.Key == ConsoleKey.Escape)
				break;
		}
	}
	
	private void Draw(){
		Console.SetCursorPosition(scorePos.x, scorePos.y);
		Console.Write("Score: " + score);
		Console.SetCursorPosition(messagePos.x, messagePos.y);
		if (drawState == DrawState.FirstHand)
			Console.Write(messageForDiscard);
		else
			Console.Write(messageForDraw);
		foreach (IDrawable drawable in drawables)
			drawable.Draw();
	}
	
	private void MoveFocusUp(){
		focusables[focusIndex].Focus(false);
		if (focusIndex < focusables.Count - 1)
			focusIndex++;
		focusables[focusIndex].Focus(true);
	}
	
	private void MoveFocusDown(){
		focusables[focusIndex].Focus(false);
		if (focusIndex > 0)
			focusIndex--;
		focusables[focusIndex].Focus(true);
	}
	
	private void RandomizeSelectedCards(){
		for (int i = 0; i < cardLayout.Count; i++)
			if ((cardLayout.GetCardAt(i) as ISelectable).IsSelected){
				int tempFocusIndex = focusables.IndexOf(cardLayout.GetCardAt(i) as IFocusable); // so the order in focusables doesn't change
				cardLayout.SetCardAt(i, deck.GetRandomCard());
				focusables[tempFocusIndex] = (cardLayout.GetCardAt(i)) as IFocusable;
			}
	}
	
	private void RandomizeCards(){
		deck = new Deck();
		if (cardLayout.Count != cardLayout.SlotCount)
			for (int i = 0; i < cardLayout.SlotCount; i++){
				cardLayout.AddCard(deck.GetRandomCard());
				focusables.Add((cardLayout.GetCardAt(i)) as IFocusable);
			}
		else
			for (int i = 0; i < cardLayout.Count; i++){
				int tempFocusIndex = focusables.IndexOf(cardLayout.GetCardAt(i) as IFocusable); // so the order in focusables doesn't change				
				cardLayout.SetCardAt(i, deck.GetRandomCard());
				focusables[tempFocusIndex] = (cardLayout.GetCardAt(i)) as IFocusable;
			}
	}
	
	
}