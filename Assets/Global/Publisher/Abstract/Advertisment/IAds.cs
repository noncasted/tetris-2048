using Cysharp.Threading.Tasks;

namespace Global.Publisher.Abstract.Advertisment
{
    public interface IAds
    {
        void ShowInterstitial();
        UniTask<RewardAdResult> ShowRewarded();
    }
}