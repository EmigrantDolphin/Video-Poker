
namespace Video_Poker {

    interface ISelectable {
        void Select(bool isSelected);
        bool IsSelected { get; }
    }
}
