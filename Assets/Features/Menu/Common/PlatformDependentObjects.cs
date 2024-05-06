using Internal.Services.Options.Implementations;
using UnityEngine;

namespace Features.Menu.Common
{
    [DisallowMultipleComponent]
    public class PlatformDependentObjects : MonoBehaviour
    {
        [SerializeField] private GameObject _mobile;
        [SerializeField] private GameObject _desktop;

        public void SetPlatform(PlatformOptions options)
        {
            if (options.IsMobile == true)
            {
                _mobile.SetActive(true);
                _desktop.SetActive(false);
            }
            else
            {
                _mobile.SetActive(false);
                _desktop.SetActive(true);
            }
        }
    }
}