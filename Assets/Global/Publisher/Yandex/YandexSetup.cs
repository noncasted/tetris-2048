using System.Collections.Generic;
using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.Advertisment;
using Global.Publisher.Abstract.DataStorages;
using Global.Publisher.Abstract.Languages;
using Global.Publisher.Abstract.Leaderboards;
using Global.Publisher.Abstract.Purchases;
using Global.Publisher.Abstract.Reviews;
using Global.Publisher.Yandex.Advertisement;
using Global.Publisher.Yandex.Common;
using Global.Publisher.Yandex.DataStorages;
using Global.Publisher.Yandex.Debugs;
using Global.Publisher.Yandex.Debugs.Ads;
using Global.Publisher.Yandex.Debugs.Purchases;
using Global.Publisher.Yandex.Debugs.Reviews;
using Global.Publisher.Yandex.Languages;
using Global.Publisher.Yandex.Leaderboard;
using Global.Publisher.Yandex.Purchases;
using Global.Publisher.Yandex.Review;
using Global.Saves;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Internal.Services.Options.Implementations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Publisher.Yandex
{
    [InlineEditor]
    public class YandexSetup : ScriptableObject, IServiceFactory
    {
        [SerializeField] private YandexCallbacks _callbacksPrefab;
        
        [SerializeField] [CreateSO] private SceneData _debugScene;
        [SerializeField] [CreateSO] private ShopProductsRegistry _productsRegistry;
        [SerializeField] [CreateSO] private ProductLink _adsDisableProduct;

        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var yandexCallbacks = Instantiate(_callbacksPrefab, Vector3.zero, Quaternion.identity);
            yandexCallbacks.name = "YandexCallbacks";

            services.RegisterComponent(yandexCallbacks);
            RegisterModules(services);

            var options = utils.Options.GetOptions<PlatformOptions>();

            if (options.IsEditor == true)
                return RegisterEditorApis(services, utils.SceneLoader, yandexCallbacks);

            RegisterBuildApis(services);

            return UniTask.CompletedTask;
        }

        private void RegisterModules(IServiceCollection builder)
        {
            builder.Register<Ads>()
                .WithParameter<IProductLink>(_adsDisableProduct)
                .As<IAds>()
                .AsCallbackListener();

            var saves = GetSaves();

            builder.Register<DataStorage>()
                .As<IDataStorage>()
                .WithParameter(saves)
                .AsCallbackListener();

            builder.Register<SystemLanguageProvider>()
                .As<ISystemLanguageProvider>();

            builder.Register<LeaderboardsProvider>()
                .As<ILeaderboardsProvider>();

            builder.Register<Reviews>()
                .As<IReviews>();

            builder.Register<Payments>()
                .WithParameter(_productsRegistry)
                .As<IPayments>();
        }

        private async UniTask RegisterEditorApis(
            IServiceCollection builder,
            ISceneLoader sceneLoader,
            YandexCallbacks callbacks)
        {
            var canvas = await sceneLoader.LoadTyped<YandexDebugCanvas>(_debugScene);
            canvas.Ads.Construct(callbacks);
            canvas.Reviews.Construct(callbacks);
            canvas.Purchase.Construct(callbacks);

            builder.Register<AdsDebugAPI>()
                .As<IAdsAPI>()
                .WithParameter<IAdsDebug>(canvas.Ads);

            builder.Register<StorageDebugAPI>()
                .As<IStorageAPI>();

            builder.Register<LanguageDebugAPI>()
                .As<ILanguageAPI>();

            builder.Register<LeaderboardsDebugAPI>()
                .As<ILeaderboardsAPI>();

            builder.Register<PurchasesDebugAPI>()
                .As<IPurchasesAPI>()
                .WithParameter(_productsRegistry)
                .WithParameter<IPurchaseDebug>(canvas.Purchase);

            builder.Register<ReviewsDebugAPI>()
                .As<IReviewsAPI>()
                .WithParameter<IReviewsDebug>(canvas.Reviews);
        }

        private void RegisterBuildApis(IServiceCollection builder)
        {
            builder.Register<AdsExternAPI>()
                .As<IAdsAPI>();

            builder.Register<StorageExternAPI>()
                .As<IStorageAPI>();

            builder.Register<LanguageExternAPI>()
                .As<ILanguageAPI>();

            builder.Register<LeaderboardsExternAPI>()
                .As<ILeaderboardsAPI>();

            builder.Register<PurchasesExternAPI>()
                .As<IPurchasesAPI>();

            builder.Register<ReviewsExternAPI>()
                .As<IReviewsAPI>();
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