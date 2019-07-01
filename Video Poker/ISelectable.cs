﻿
namespace VideoPoker {

    public interface ISelectable {
        void Select(bool isSelected);
        bool IsSelected { get; }
    }
}
