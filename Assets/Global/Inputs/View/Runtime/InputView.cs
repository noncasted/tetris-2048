using Common.DataTypes.Runtime.Reactive;
using Global.Inputs.View.Abstract;
using Global.Inputs.View.Runtime.Actions;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Runtime.Lifetimes;

namespace Global.Inputs.View.Runtime
{
    public class InputView :
        IInputView,
        IInputViewRebindCallbacks,
        IScopeAwakeListener
    {
        public InputView(
            InputCallbacks callbacks,
            IUpdater updater,
            InputActions inputActions,
            Controls controls)
        {
            _callbacks = callbacks;
            _updater = updater;
            _inputActions = inputActions;
            _controls = controls;
        }

        private readonly InputCallbacks _callbacks;
        private readonly IUpdater _updater;
        private readonly InputActions _inputActions;
        private readonly Controls _controls;

        private readonly ViewableProperty<ILifetime> _lifetime = new();

        public IViewableProperty<ILifetime> ListenLifetime => _lifetime;

        public void OnAwake()
        {
            _lifetime.Set(new Lifetime());
            _controls.Enable();
            _callbacks.Invoke(_lifetime.Value);
            _updater.Add(_inputActions);
        }

        public void OnBeforeRebind()
        {
            _lifetime.Value.Terminate();
            _lifetime.Set(new Lifetime());
        }

        public void OnAfterRebind()
        {
        }
    }
}