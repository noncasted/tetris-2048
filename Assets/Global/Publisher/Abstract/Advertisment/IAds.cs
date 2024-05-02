using Cysharp.Threading.Tasks;

namespace Global.Publisher.Abstract.Advertisment
{
    public interface IAds
    {
        UniTask ShowInterstitial();
        UniTask<RewardAdResult> ShowRewarded();
    }
}