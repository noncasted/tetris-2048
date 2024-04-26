namespace Global.System.Updaters.Abstract
{
    public interface IUpdateSpeedSetter
    {
        void Set(float speed);

        void Pause();
        void Continue();
    }
}