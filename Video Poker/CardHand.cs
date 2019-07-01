using System.Collections.Generic;
using System.Collections;


namespace VideoPoker {
    public class CardHand : IEnumerable, IEnumerator {
        Vector2 pos = new Vector2();
        int width = 0;
        int height = 0;
        int xOffset = 3;

        int position = -1; //for IEnumerable
        readonly int maxSlots = 5;
        public int MaxSlots { get { return maxSlots; } }
        List<ICard> cards;
        Deck deck;

        public CardHand() {
            deck = new Deck();
            cards = new List<ICard>();
            for (int i = 0; i < maxSlots; i++)
                cards.Add(new Card());
            PositionCards();
            RandomizeCards();
        }

        private void PositionCards() {
            for (int i = 0; i < maxSlots; i++)
                cards[i].Position = new Vector2(pos.x + i * cards[i].Width + xOffset * i, pos.y);
            width = cards[maxSlots - 1].Position.x + cards[maxSlots - 1].Width - pos.x; // last added card in mind
            height = cards[maxSlots - 1].Height;
        }

        public void RandomizeSelectedCards() {
            for (int i = 0; i < cards.Count; i++)
                if ((cards[i] as ISelectable).IsSelected) {
                    ICard tempCard = deck.GetRandomCard();
                    cards[i].Rank = tempCard.Rank;
                    cards[i].Type = tempCard.Type;
                    (cards[i] as ISelectable).Select(false);
                }
        }

        public void RandomizeCards() {
            deck = new Deck();
            
            for (int i = 0; i < cards.Count; i++) {
                ICard tempCard = deck.GetRandomCard();
                cards[i].Rank = tempCard.Rank;
                cards[i].Type = tempCard.Type;
                (cards[i] as ISelectable).Select(false);
            }
        }

        public ICard this[int index] {
            get { return cards[index]; }
        }
        public List<ICard> CardList {
            get { return cards; }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public Vector2 Position {
            get { return pos; }
            set {
                pos = value;
                PositionCards();
            }
        }
        public ICard First {
            get { return cards[0]; }
            set { cards[0] = value; }
        }
        public ICard Second {
            get { return cards[1]; }
            set { cards[1] = value; }
        }
        public ICard Third {
            get { return cards[2]; }
            set { cards[2] = value; }
        }
        public ICard Fourth {
            get { return cards[3]; }
            set { cards[3] = value; }
        }
        public ICard Fifth {
            get { return cards[4]; }
            set { cards[4] = value; }
        }

        public bool MoveNext() {
            position++;
            return (position < cards.Count);
        }

        public void Reset() {
            position = -1;
        }
        
        public object Current {
            get { return cards[position]; }
        }

        public IEnumerator GetEnumerator() {
            return (IEnumerator)this;
        }

    }
}
