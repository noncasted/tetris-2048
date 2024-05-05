using System.Runtime.InteropServices;
using Internal.Scopes.Abstract.Instances.Services;

namespace Global.Publisher.Yandex
{
    public class YandexInitialization : IScopeAwakeListener
    {
        [DllImport("__Internal")]
        private static extern string InitializeYandexSDK();
        
        public void OnAwake()
        {
            InitializeYandexSDK();
        }
    }
}