using Global.UI.Overlays.Abstract;
using UnityEngine;

namespace Global.UI.Overlays.Runtime
{
    [DisallowMultipleComponent]
    public class GlobalOverlayView : MonoBehaviour
    {
        [SerializeField] private GlobalExceptionView _exceptionView;

        public IGlobalExceptionView ExceptionView => _exceptionView;

        private void Awake()
        {
            _exceptionView.gameObject.SetActive(false);
        }
    }
}