using Internal.Scopes.Abstract.Options;
using VContainer;

namespace Internal.Setup.Abstract
{
    public interface IInternalServiceFactory
    {
        void Create(IOptions options, IContainerBuilder builder);
    }
}