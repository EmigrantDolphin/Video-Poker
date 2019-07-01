using System;

namespace VideoPoker {
    public class Card : ICard, ISelectable, IFocusable, IDrawable {
        Vector2 pos = new Vector2();
        readonly int width = 15, height = 15;
        readonly Vector2 rankPos = new Vector2(2, 2);
        readonly Vector2 typePos = new Vector2(4, 7);
        CardRank rank;
        CardType type;

        bool isSelected = false;
        bool isFocused = false;

        string wallSimbol = "o";
        string focusedWallSimbol = "O";
        string unfocusedWallSimbol = "o";

        ConsoleColor frameColor = ConsoleColor.Black;
        ConsoleColor selectedFrameColor = ConsoleColor.Red;
        ConsoleColor unSelectedFrameColor = ConsoleColor.Black;

        ConsoleColor cardColor = ConsoleColor.Black;

        public Card() {
        }
        public Card(CardRank rank, CardType type) {
            this.rank = rank;
            this.type = type;
            if (type == CardType.Hearts || type == CardType.Diamonds)
                cardColor = ConsoleColor.Red;
        }

        public Vector2 Position {
            get { return pos; }
            set { pos = value; }
        }

        public int Width {
            get { return width; }
        }
        public int Height {
            get { return height; }
        }

        public void Draw() {
            //draw frame
            Console.ForegroundColor = frameColor;
            for (int i = pos.x; i < width + pos.x; i++) {
                Console.SetCursorPosition(i, pos.y);
                Console.Write(wallSimbol);
                Console.SetCursorPosition(i, pos.y + height - 1);
                Console.Write(wallSimbol);
            }
            for (int i = pos.y; i < height + pos.y; i++) {
                Console.SetCursorPosition(pos.x, i);
                Console.Write(wallSimbol);
                Console.SetCursorPosition(pos.x + width - 1, i);
                Console.Write(wallSimbol);
            }
            Console.ForegroundColor = unSelectedFrameColor;

            //draw rank and type

            Console.ForegroundColor = cardColor;
            Console.SetCursorPosition(pos.x + rankPos.x, pos.y + rankPos.y);
            Console.Write(rank);
            Console.SetCursorPosition(pos.x + width - rankPos.x - rank.ToString().Length, pos.y + (height - 1) - rankPos.y);
            Console.Write(rank);
            Console.SetCursorPosition(pos.x + typePos.x, pos.y + typePos.y);
            Console.Write(type);
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public CardRank Rank {
            get { return rank; }
            set { rank = value; }
        }
        public CardType Type {
            get { return type; }
            set {
                type = value;
                if (type == CardType.Hearts || type == CardType.Diamonds)
                    cardColor = ConsoleColor.Red;
                else
                    cardColor = ConsoleColor.Black;
            }
        }

        public void Focus(bool isFocused) {
            this.isFocused = isFocused;
            if (isFocused)
                wallSimbol = focusedWallSimbol;
            else
                wallSimbol = unfocusedWallSimbol;
        }

        public void Select(bool isSelected) {
            this.isSelected = isSelected;
            if (isSelected)
                frameColor = selectedFrameColor;
            else
                frameColor = unSelectedFrameColor;
        }

        public bool IsSelected {
            get { return isSelected; }
        }

        public void Press() {
            if (IsSelected)
                Select(false);
            else
                Select(true);
        }


    }
}
