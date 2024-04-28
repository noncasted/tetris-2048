using Cysharp.Threading.Tasks;
using Global.System.Updaters.Abstract;
using Global.System.Updaters.Delays;
using Global.System.Updaters.Progressions;
using Global.System.Updaters.Runtime;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.System.Updaters.Setup
{
    [InlineEditor]
    public class UpdaterFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] private Updater _prefab;

        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var updater = Instantiate(_prefab);
            updater.name = "Updater";

            services.RegisterComponent(updater)
                .As<IUpdater>()
                .As<IUpdateSpeedModifier>()
                .As<IUpdateSpeedSetter>()
                .AsSelfResolvable()
                .AsCallbackListener();

            services.Register<DelayRunner>()
                .As<IDelayRunner>();

            services.Register<ProgressionFactory>()
                .As<IProgressionFactory>();

            utils.Binder.MoveToModules(updater);
            
            return UniTask.CompletedTask;
        }
    }
}