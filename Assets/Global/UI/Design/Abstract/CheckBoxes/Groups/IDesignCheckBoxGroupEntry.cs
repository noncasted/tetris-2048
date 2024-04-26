namespace Global.UI.Design.Abstract.CheckBoxes.Groups
{
    public interface IDesignCheckBoxGroupEntry
    {
        string Key { get; }

        void Select();
        void Deselect();
    }
}