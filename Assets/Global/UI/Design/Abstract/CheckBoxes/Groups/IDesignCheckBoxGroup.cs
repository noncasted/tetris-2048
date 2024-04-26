using Common.DataTypes.Runtime.Reactive;

namespace Global.UI.Design.Abstract.CheckBoxes.Groups
{
    public interface IDesignCheckBoxGroup
    {
        IViewableProperty<string> Selection { get; }
    }
}