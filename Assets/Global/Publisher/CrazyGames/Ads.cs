using CrazyGames;
using Cysharp.Threading.Tasks;
using Global.Audio.Player.Abstract;
using Global.Publisher.Abstract.Advertisment;
using Global.System.Updaters.Abstract;
using UnityEngine;

namespace Global.Publisher.CrazyGames
{
    public class Ads : IAds
    {
        public Ads(
            IGlobalVolume volume,
            IUpdateSpeedSetter speedSetter)
        {
            _volume = volume;
            _speedSetter = speedSetter;
        }

        private readonly IGlobalVolume _volume;
        private readonly IUpdateSpeedSetter _speedSetter;

        public async UniTask ShowInterstitial()
        {
            var completion = new UniTaskCompletionSource();

            _speedSetter.Pause();
            _volume.Mute();

            CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () => { }, OnFail, OnSuccess);
            await completion.Task;

            _volume.Unmute();
            _speedSetter.Continue();

            return;

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

        public async UniTask<RewardAdResult> ShowRewarded()
        {
            var completion = new UniTaskCompletionSource<RewardAdResult>();

            _speedSetter.Pause();
            _volume.Mute();

            CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () => { }, OnFail, OnSuccess);

            var result = await completion.Task;

            _volume.Unmute();
            _speedSetter.Continue();

            return result;

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