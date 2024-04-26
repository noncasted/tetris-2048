using Internal.Scopes.Abstract.Environments;
using Internal.Scopes.Abstract.Options;

namespace Internal.Services.Options.Implementations
{
    public class PlatformOptions : IOptionsEntry
    {
        public PlatformOptions(PlatformType platformType)
        {
            PlatformType = platformType;
        }

        public PlatformType PlatformType { get; }

        public bool IsEditor
        {
            get
            {
#if UNITY_EDITOR
                return true;
#endif
                return false;
            }
        }
    }
}