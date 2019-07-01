using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Poker {
    class Card : ICard, ISelectable, IFocusable, IDrawable {
        Vector2i pos = new Vector2i();
        readonly int WIDTH = 15, HEIGHT = 15;
        readonly Vector2i RANKPOS = new Vector2i(2, 2);
        readonly Vector2i TYPEPOS = new Vector2i(4, 7);
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

        public Vector2i Position {
            get { return pos; }
            set { pos = value; }
        }

        public int Width {
            get { return WIDTH; }
        }
        public int Height {
            get { return HEIGHT; }
        }

        public void Draw() {
            //draw frame
            Console.ForegroundColor = frameColor;
            for (int i = pos.x; i < WIDTH + pos.x; i++) {
                Console.SetCursorPosition(i, pos.y);
                Console.Write(wallSimbol);
                Console.SetCursorPosition(i, pos.y + HEIGHT - 1);
                Console.Write(wallSimbol);
            }
            for (int i = pos.y; i < HEIGHT + pos.y; i++) {
                Console.SetCursorPosition(pos.x, i);
                Console.Write(wallSimbol);
                Console.SetCursorPosition(pos.x + WIDTH - 1, i);
                Console.Write(wallSimbol);
            }
            Console.ForegroundColor = unSelectedFrameColor;

            //draw rank and type

            Console.ForegroundColor = cardColor;
            Console.SetCursorPosition(pos.x + RANKPOS.x, pos.y + RANKPOS.y);
            Console.Write(rank);
            Console.SetCursorPosition(pos.x + WIDTH - RANKPOS.x - rank.ToString().Length, pos.y + (HEIGHT - 1) - RANKPOS.y);
            Console.Write(rank);
            Console.SetCursorPosition(pos.x + TYPEPOS.x, pos.y + TYPEPOS.y);
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
