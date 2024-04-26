using Common.Tools.SceneServices;
using Internal.Scopes.Abstract.Containers;
using Menu.GameEnds.Abstract;
using Menu.GameEnds.Runtime;
using Menu.Leaderboards.Abstract;
using Menu.Leaderboards.Runtime;
using Menu.Loop.Abstract;
using Menu.Main.Abstract;
using Menu.Main.Runtime;
using Menu.Settings.Abstract;
using Menu.Settings.Runtime;
using UnityEngine;

namespace Menu.Loop.Runtime
{
    [DisallowMultipleComponent]
    public class MenuSceneServicesFactory : SceneServicesFactory
    {
        [SerializeField] private MenuMain _main;
        [SerializeField] private MenuSettings _settings;
        [SerializeField] private MenuAbout _about;
        [SerializeField] private MenuGameEnd _gameEnd;
        [SerializeField] private MenuNavigation _navigation;

        protected override void CollectServices(IServiceCollection services)
        {
            services.RegisterComponent(_main)
                .As<IMenuMain>();

            services.RegisterComponent(_settings)
                .As<IMenuSettings>();

            services.RegisterComponent(_about)
                .As<IMenuAbout>();

            services.RegisterComponent(_gameEnd)
                .As<IMenuGameEnd>();

            services.RegisterComponent(_navigation)
                .As<IMenuNavigation>();
        }
    }
}