using Common.Tools.SceneServices;
using Features.Menu.GameEnds.Abstract;
using Features.Menu.GameEnds.Runtime;
using Features.Menu.Leaderboards.Abstract;
using Features.Menu.Leaderboards.Runtime;
using Features.Menu.Navigation.Abstract;
using Features.Menu.Navigation.Runtime;
using Features.Menu.Settings.Abstract;
using Features.Menu.Settings.Runtime;
using Internal.Scopes.Abstract.Containers;
using UnityEngine;

namespace Features.Menu.Loop.Runtime
{
    [DisallowMultipleComponent]
    public class MenuSceneServicesFactory : SceneServicesFactory
    {
        [SerializeField] private MenuSettings _settings;
        [SerializeField] private MenuAbout _about;
        [SerializeField] private MenuGameEnd _gameEnd;
        [SerializeField] private MenuNavigation _navigation;

        protected override void CollectServices(IServiceCollection services)
        {
            services.RegisterComponent(_settings)
                .As<IMenuSettings>()
                .AsCallbackListener();

            services.RegisterComponent(_about)
                .As<IMenuAbout>()
                .AsCallbackListener();

            services.RegisterComponent(_gameEnd)
                .As<IMenuGameEnd>()
                .AsCallbackListener();

            services.RegisterComponent(_navigation)
                .As<IMenuNavigation>()
                .AsCallbackListener();
        }
    }
}