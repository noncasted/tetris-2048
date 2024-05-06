using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.Advertisment;
using Global.Publisher.Abstract.DataStorages;
using Global.Publisher.Abstract.Languages;
using Global.Saves;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Publisher.CrazyGames
{
    [InlineEditor]
    public class CrazyGamesSetup : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<CrazyGamesInitialization>()
                .AsCallbackListener();

            services.Register<Ads>()
                .As<IAds>();
            
            services.Register<DataStorage>()
                .WithParameter(GetSaves())
                .As<IDataStorage>()
                .AsCallbackListener();

            services.Register<LanguageProvider>()
                .As<ISystemLanguageProvider>();
            
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