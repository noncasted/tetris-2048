using Cysharp.Threading.Tasks;
using Global.Inputs.View.Abstract;
using Global.Inputs.View.Runtime.Actions;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Lean.Touch;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Inputs.View.Runtime
{
    [InlineEditor]
    public class InputViewFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] private LeanTouch _leanTouch;
        
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var leanTouch = Instantiate(_leanTouch);
            leanTouch.name = "LeanTouch";
            utils.Binder.MoveToModules(leanTouch);
            services.RegisterComponent(leanTouch);
            
            var controls = new Controls();

            services.RegisterInstance(controls);
            services.RegisterInstance(controls.GamePlay);
            services.RegisterInstance(controls.AssemblyGraph);

            var callbacks = new InputCallbacks();
            utils.Callbacks.AddCustomListener(callbacks);

            services.Register<InputView>()
                .WithParameter(controls)
                .WithParameter(callbacks)
                .AsImplementedInterfaces()
                .AsCallbackListener();

            services.Register<InputActions>()
                .As<IInputActions>()
                .AsCallbackListener();
            
            return UniTask.CompletedTask;
        }
    }
}