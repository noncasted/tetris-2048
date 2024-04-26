namespace Loop.Abstract
{
    public interface IGameLoop
    {
        GameSpeed Speed { get; }
        
        void SetGameSpeed(GameSpeed speed);
    }
}