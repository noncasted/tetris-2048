using Cysharp.Threading.Tasks;

namespace Global.Publisher.Abstract.Reviews
{
    public interface IReviews
    {
        UniTask Review();
    }
}