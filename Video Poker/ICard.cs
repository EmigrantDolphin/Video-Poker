using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Poker {

    interface ICard {
        Vector2i Position { get; set; }
        int Width { get; }
        int Height { get; }
        void Draw();
        CardRank Rank { get; set; }
        CardType Type { get; set; }
    }
}
