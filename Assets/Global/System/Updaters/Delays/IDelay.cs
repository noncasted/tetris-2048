using Cysharp.Threading.Tasks;

namespace Global.System.Updaters.Delays
{
    public interface IDelay
    {
        UniTask Run();
    }
}