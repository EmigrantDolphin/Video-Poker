using System.Collections.Generic;
using System;

//singleton pattern
class PointDistributer {
	
	SortedDictionary<CardRank, int> rankCounter;
	List<ICard> cards;
	
	
	private static readonly PointDistributer INSTANCE = new PointDistributer();
	
	private PointDistributer() {}
	
	public static KeyValuePair<string, int> EvaluateHand (List<ICard> cards){
		return INSTANCE.Evaluate(cards);
	}
	
	private KeyValuePair<string, int> Evaluate(List<ICard> cards){
		this.cards = new List<ICard>(cards);
		PopulateRankCounter();
		SortByRank();
		
		
		if (IsRoyalFlush())
			return new KeyValuePair<string, int>("Royal Flush!", 800);
		if (IsStraightFlush())
			return new KeyValuePair<string, int>("Straigh Flush!", 50);
		if (IsFourOfAKind())
			return new KeyValuePair<string, int>("Four Of A Kind!", 25);
		if (IsFullHouse())
			return new KeyValuePair<string, int>("Full House!", 9);
		if (IsFlush())
			return new KeyValuePair<string, int>("Flush!", 6);
		if (IsStraight())
			return new KeyValuePair<string, int>("Straight!", 4);
		if (IsThreeOfAKind())
			return new KeyValuePair<string, int>("Three Of A Kind!", 3);
		if (IsTwoPair())
			return new KeyValuePair<string, int>("Two Pair!", 2);
		if (IsJacksOrBetter())
			return new KeyValuePair<string, int>("Jacks Or Better!", 1);

		return new KeyValuePair<string, int>("You win NOTHING", 0);
	}
	
	private void PopulateRankCounter(){
		rankCounter = new SortedDictionary<CardRank, int>();
		for (int i = 0; i < cards.Count; i++)
			if (!rankCounter.ContainsKey(cards[i].Rank))
				rankCounter.Add(cards[i].Rank, 0);
		for (int i = 0; i < cards.Count; i++)
			rankCounter[cards[i].Rank]++;
	}
	
	private void SortByRank(){
		for (int i = 0; i < cards.Count - 1; i++)
			for (int j = i+1; j < cards.Count; j++)
				if (cards[i].Rank > cards[j].Rank){
					ICard tempCard = cards[i];
					cards[i] = cards[j];
					cards[j] = tempCard;
				}
	}
	
	private bool IsSequential(){
		for (int i = 0; i < cards.Count - 1; i++){
			if (cards[i].Rank == CardRank.Ace) // ace can only be last card, var i here can't be last card
				return false;
			
			if (cards[i].Rank + 1 != cards[i+1].Rank)
				return false;			
		}	
		return true;
	}	
	
	private bool IsRoyalFlush(){
		if (cards[0].Rank == CardRank.Ten && IsSequential())
			return true;
		
		return false;
	}
	private bool IsStraightFlush(){
		for (int i = 0; i < cards.Count - 1; i++)
			if (cards[i].Type != cards[i+1].Type)
				return false;
			
		if (!IsSequential())
			return false;
				
		return true;
	}
	
	private bool IsFourOfAKind(){
		foreach (KeyValuePair<CardRank, int> pair in rankCounter)
			if (pair.Value == 4)
				return true;
			
		return false;
	}
	
	private bool IsFullHouse(){
		bool hasTwoSameRanks = false;
		bool hasThreeSameRanks = false;
		
		foreach(KeyValuePair<CardRank, int> pair in rankCounter){
			if (pair.Value == 2)
				hasTwoSameRanks = true;
			if (pair.Value == 3)
				hasThreeSameRanks = true;
		}
		
		if (hasTwoSameRanks && hasThreeSameRanks)
			return true;
		
		return false;
	}
	
	private bool IsFlush(){
		for (int i = 0; i < cards.Count - 1; i++)
			if (cards[i].Type != cards[i+1].Type)
				return false;
		
		return true;
	}
	
	private bool IsStraight(){
		if (IsSequential())
			return true;
		return false;
	}
	
	private bool IsThreeOfAKind(){
		foreach (KeyValuePair<CardRank, int> pair in rankCounter)
			if (pair.Value == 3)
				return true;
		return false;
	}
	
	private bool IsTwoPair(){
		foreach (KeyValuePair<CardRank, int> pair in rankCounter)
			if (pair.Value == 2)
				return true;
		return false;
	}
	
	private bool IsJacksOrBetter(){
		int jacksOrBetterCounter = 0;
		for (int i = 0; i < cards.Count; i++)
			if (cards[i].Rank >= CardRank.Jack)
				jacksOrBetterCounter++;
		if (jacksOrBetterCounter >= 2)
			return true;
		return false;
	}
	
	
}