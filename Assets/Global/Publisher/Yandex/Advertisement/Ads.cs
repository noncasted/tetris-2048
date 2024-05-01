using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.Advertisment;
using Global.Publisher.Abstract.DataStorages;
using Global.Publisher.Abstract.Purchases;
using Global.Publisher.Yandex.Common;
using Global.Saves;
using Global.System.MessageBrokers.Abstract;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.Publisher.Yandex.Advertisement
{
    public class Ads : IAds, IScopeEnableAsyncListener, IScopeLifetimeListener
    {
        private Ads(
            YandexCallbacks callbacks,
            IDataStorage dataStorage,
            IAdsAPI api,
            IProductLink adsProduct,
            IUpdateSpeedSetter speedSetter)
        {
            _callbacks = callbacks;
            _dataStorage = dataStorage;
            _api = api;
            _adsProduct = adsProduct;
            _speedSetter = speedSetter;
        }

        private readonly IAdsAPI _api;
        private readonly IProductLink _adsProduct;
        private readonly IUpdateSpeedSetter _speedSetter;
        private readonly IDataStorage _dataStorage;
        private readonly YandexCallbacks _callbacks;

        private AdsSave _save;

        public async UniTask OnEnabledAsync()
        {
            _save = await _dataStorage.GetEntry<AdsSave>();
        }
        
        public void OnLifetimeCreated(ILifetime lifetime)
        {
            Msg.Listen<PurchaseEvent>(lifetime, OnProductUnlocked);
        }

        public void ShowInterstitial()
        {
            ProcessInterstitial().Forget();
        }

        public async UniTask<RewardAdResult> ShowRewarded()
        {
            _speedSetter.Pause();   
            var handler = new RewardedHandler(_callbacks, _api);
            var result = await handler.Show();
            _speedSetter.Continue();   

            return result;
        }

        private async UniTaskVoid ProcessInterstitial()
        {
            if (_save.IsDisabled == true)
                return;
            
            _speedSetter.Pause();   
           
            var handler = new InterstitialHandler(_callbacks, _api);
            await handler.Show();
            
            _speedSetter.Continue();    
        }

        private void OnProductUnlocked(PurchaseEvent purchase)
        {
            if (purchase.ProductLink != _adsProduct)
                return;

            _save.IsDisabled = false;
            _dataStorage.Save(_save).Forget();
        }
    }
}