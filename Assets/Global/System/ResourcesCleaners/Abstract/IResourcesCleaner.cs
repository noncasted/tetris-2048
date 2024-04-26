using Cysharp.Threading.Tasks;

namespace Global.System.ResourcesCleaners.Abstract
{
    public interface IResourcesCleaner
    {
        UniTask CleanUp();
    }
}