
interface ICard{
	Vector2i Position{get; set;}
	int Width{get;}
	int Height{get;}
	void Draw();
	CardRank Rank{get; set;}
	CardType Type{get; set;}
}