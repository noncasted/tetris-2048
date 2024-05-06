using Global.Publisher.Yandex.Debugs.Ads;

namespace Global.Publisher.Yandex.Advertisement
{
    public class AdsDebugAPI : IAdsAPI
    {
        public AdsDebugAPI(IAdsDebug debug)
        {
            _debug = debug;
        }

        private readonly IAdsDebug _debug;

        public void ShowInterstitial_Internal()
        {
            _debug.ShowInterstitial();
        }

        public void ShowRewarded_Internal()
        {
            _debug.ShowRewarded();
        }
    }
}