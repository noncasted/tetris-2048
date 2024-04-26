using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Save.Abstract;
using Global.Audio.Player.Runtime;
using Global.Publisher.Abstract.Bootstrap;
using Global.Publisher.Abstract.Callbacks;
using Global.Publisher.Abstract.DataStorages;
using Global.Publisher.Abstract.Languages;
using Global.Publisher.Abstract.Purchases;
using Global.Publisher.Web.Common;
using Global.Publisher.Web.DataStorages;
using Global.Publisher.Web.Languages;
using Global.UI.Localizations.Runtime;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Services.Options.Implementations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Publisher.Web.Bootstrap
{
    [InlineEditor]
    public class WebFactory : PublisherSdkFactory
    {
        [SerializeField] private WebCallbacks _callbacksPrefab;

        public override async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var options = utils.Options.GetOptions<PlatformOptions>();

            var callbacks = Instantiate(_callbacksPrefab);

            services.RegisterInstance(callbacks)
                .As<IJsErrorCallback>();

            services.Register<WebDataStorage>()
                .As<IDataStorage>()
                .WithParameter(GetSaves())
                .AsCallbackListener();

            services.Register<WebSystemLanguageProvider>()
                .As<ISystemLanguageProvider>()
                .AsCallbackListener();

            if (true)
            {
                services.Register<WebLanguageDebugAPI>()
                    .As<IWebLanguageAPI>();
            }
            else
            {
                services.Register<WebLanguageExternAPI>()
                    .As<IWebLanguageAPI>();
            }
        }

        private IReadOnlyList<IStorageEntrySerializer> GetSaves()
        {
            return new IStorageEntrySerializer[]
            {
                new VolumeSaveSerializer(),
                new LanguageSaveSerializer(),
                new PurchasesSaveSerializer(),
                new BoardSaveSerializer()
            };
        }
    }
}