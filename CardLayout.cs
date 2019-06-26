using System.Collections.Generic;

class CardLayout : IDrawable{
	
	Vector2i pos;
	int width = 0, height = 0;
	int xOffset = 3;
	int slotCount = 5;
	List<ICard> cards;
	
	public CardLayout(){
		pos = new Vector2i();
		cards = new List<ICard>();
	}
	
	public void AddCard(ICard card){
		if (cards.Count >= slotCount)
			return;
		card.Position = new Vector2i(pos.x + cards.Count * card.Width + xOffset * cards.Count, pos.y);
		width = card.Position.x + card.Width - pos.x; // last added card in mind
		height = card.Height;
		cards.Add(card);
	}
	
	public int Count{
		get { return cards.Count; }
	}
	public int SlotCount{
		get { return slotCount; }
	}
	public int Height{
		get { return height; }
	}
	public int Width{
		get { return width; }
	}
	
	public List<ICard> Cards{
		get { return cards; }
	}
	public void Clear(){
		width = 0;
		height = 0;
		cards.Clear();
	}
	
	public ICard GetCardAt(int num){
		return cards[num];
	}
	public void SetCardAt(int num, ICard card){
		card.Position = new Vector2i(pos.x + num * card.Width + xOffset * num, pos.y);
		cards[num] = card;
	}
	
	public Vector2i Position{
		get { return pos; }
		set { pos = value; }
	}
	
	public void Draw(){
		foreach (ICard card in cards)
			card.Draw();
	}
	
}