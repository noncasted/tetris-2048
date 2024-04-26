using Cysharp.Threading.Tasks;

namespace Global.Publisher.Abstract.DataStorages
{
    public interface IDataStorage
    {
        UniTask<T> GetEntry<T>() where T : class;
        UniTask Save<T>(T data);
    }
}