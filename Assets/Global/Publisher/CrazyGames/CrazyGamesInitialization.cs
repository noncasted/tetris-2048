using CrazyGames;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Instances.Services;

namespace Global.Publisher.CrazyGames
{
    public class CrazyGamesInitialization : IScopeAwakeAsyncListener
    {
        public async UniTask OnAwakeAsync()
        {
            var completion = new UniTaskCompletionSource();
            CrazySDK.Init(OnInitialized);
            await completion.Task;

            return;

            void OnInitialized()
            {
                completion.TrySetResult();
            }
        }
    }
}