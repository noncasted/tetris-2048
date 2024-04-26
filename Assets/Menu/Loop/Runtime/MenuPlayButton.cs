using UnityEngine;

namespace Menu.Loop.Runtime
{
    [DisallowMultipleComponent]
    public class MenuPlayButton : MonoBehaviour
    {
        [SerializeField] private GameObject _playIcon;
        [SerializeField] private GameObject _pauseIcon;

        public void SetToPlay()
        {
            _playIcon.SetActive(true);
            _pauseIcon.SetActive(false);
        }

        public void SetToPause()
        {
            _playIcon.SetActive(false);
            _pauseIcon.SetActive(true);
        }
    }
}