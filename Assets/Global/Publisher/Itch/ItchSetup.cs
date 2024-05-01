using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.Advertisment;
using Global.Publisher.Abstract.Callbacks;
using Global.Publisher.Abstract.DataStorages;
using Global.Publisher.Abstract.Languages;
using Global.Publisher.Itch.Common;
using Global.Publisher.Itch.Language;
using Global.Saves;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Services.Options.Implementations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Publisher.Itch
{
    [InlineEditor]
    public class ItchSetup : ScriptableObject, IServiceFactory
    {
        [SerializeField] private ItchCallbacks _callbacksPrefab;

        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var options = utils.Options.GetOptions<PlatformOptions>();

            var callbacks = Instantiate(_callbacksPrefab);

            services.Register<ItchAds>()
                .As<IAds>();
            
            services.RegisterInstance(callbacks)
                .As<IJsErrorCallback>();

            services.Register<ItchDataStorage>()
                .As<IDataStorage>()
                .WithParameter(GetSaves())
                .AsCallbackListener();

            services.Register<ItchLanguageProvider>()
                .As<ISystemLanguageProvider>()
                .AsCallbackListener();

            if (options.IsEditor == true)
            {
                services.Register<ItchLanguageDebugAPI>()
                    .As<IItchLanguageAPI>();
            }
            else
            {
                services.Register<ItchLanguageExternAPI>()
                    .As<IItchLanguageAPI>();
            }
            
            return UniTask.CompletedTask;
        }

        private IReadOnlyList<IStorageEntrySerializer> GetSaves()
        {
            return new IStorageEntrySerializer[]
            {
                new AdsSaveSerializer(),
                new GameSaveSerializer(),
                new LanguageSaveSerializer(),
                new PurchasesSaveSerializer(),
                new TutorialSaveSerializer(),
                new VolumeSaveSerializer(),
            };
        }
    }
}