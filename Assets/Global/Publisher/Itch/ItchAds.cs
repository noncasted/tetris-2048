using Cysharp.Threading.Tasks;
using Global.Publisher.Abstract.Advertisment;

namespace Global.Publisher.Itch
{
    public class ItchAds : IAds
    {
        public UniTask ShowInterstitial()
        {
            return UniTask.CompletedTask;
        }

        public UniTask<RewardAdResult> ShowRewarded()
        {
            return default;
        }
    }
}