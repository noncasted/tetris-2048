using Cysharp.Threading.Tasks;

namespace Internal.Scopes.Abstract.Callbacks
{
    public interface ICallbacksStage
    {
        public void Add(ICallbackEntity handler);

        public void Listen(object listener);

        public UniTask Run();
    }
}