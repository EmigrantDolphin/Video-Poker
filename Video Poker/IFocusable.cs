using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Poker {
    interface IFocusable {
        void Focus(bool isFocused);
        void Press();
    }
}
