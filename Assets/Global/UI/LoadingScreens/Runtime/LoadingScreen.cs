using Global.UI.LoadingScreens.Abstract;
using UnityEngine;

namespace Global.UI.LoadingScreens.Runtime
{
    [DisallowMultipleComponent]
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _gameLoad; 

        private void Awake()
        {
            _canvas.SetActive(false);
        }

        public void HideGameLoading()
        {
            _gameLoad.SetActive(false);
        }

        public void Show()
        {
            _canvas.SetActive(true);
        }

        public void Hide()
        {
            _canvas.SetActive(false);
        }
    }
}