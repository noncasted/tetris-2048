using Common.DataTypes.Runtime.Reactive;

namespace Global.UI.Design.Abstract.CheckBoxes
{
    public interface IDesignCheckBox
    {
        IViewableProperty<bool> IsChecked { get; }

        void Lock();
        void Unlock();

        void Uncheck();
    }
}