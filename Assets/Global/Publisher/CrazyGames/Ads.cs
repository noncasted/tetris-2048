using CrazyGames;
using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.Advertisment;
using UnityEngine;

namespace Global.Publisher.CrazyGames
{
    public class Ads : IAds
    {
        public UniTask ShowInterstitial()
        {
            var completion = new UniTaskCompletionSource();
            CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () => { }, OnFail, OnSuccess);

            return default;

            void OnFail(SdkError error)
            {
                Debug.LogError(error.message);
                completion.TrySetResult();
            }

            void OnSuccess()
            {
                completion.TrySetResult();
            }
        }

        public UniTask<RewardAdResult> ShowRewarded()
        {
            var completion = new UniTaskCompletionSource<RewardAdResult>();
            CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () => { }, OnFail, OnSuccess);

            return default;

            void OnFail(SdkError error)
            {
                Debug.LogError(error.message);
                completion.TrySetResult(RewardAdResult.Error);
            }

            void OnSuccess()
            {
                completion.TrySetResult(RewardAdResult.Applied);
            }
        }
    }
}