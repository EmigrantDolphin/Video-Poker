
namespace VideoPoker {

    public interface ICard {
        Vector2 Position { get; set; }
        int Width { get; }
        int Height { get; }
        void Draw();
        CardRank Rank { get; set; }
        CardType Type { get; set; }
    }
}
