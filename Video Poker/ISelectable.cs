using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Poker {

    interface ISelectable {
        void Select(bool isSelected);
        bool IsSelected { get; }
    }
}
