using Common.DataTypes.Runtime.Reactive;
using Common.DataTypes.Runtime.Structs;

namespace Features.GamePlay.Input.Abstract
{
    public interface IGameInput
    {
        IViewableDelegate<CoordinateDirection> Swipe { get; }
    }
}