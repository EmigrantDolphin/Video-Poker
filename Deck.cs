using System.Collections.Generic;
using System;

class Deck{
	Random rnd = new Random();

	List<ICard> cardList;
	public Deck(){
		cardList = new List<ICard>();
		foreach (CardType type in Enum.GetValues(typeof(CardType)))
			foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
				cardList.Add(new Card(rank, type));
	}
	
	public ICard GetRandomCard(){
		int randomNumber = rnd.Next(0, cardList.Count);
		ICard card = cardList[randomNumber];
		cardList.RemoveAt(randomNumber);
		return card;
	}
	
}