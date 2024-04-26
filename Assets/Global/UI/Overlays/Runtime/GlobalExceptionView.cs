using Global.UI.Overlays.Abstract;
using TMPro;
using UnityEngine;

namespace Global.UI.Overlays.Runtime
{
    [DisallowMultipleComponent]
    public class GlobalExceptionView : MonoBehaviour, IGlobalExceptionView
    {
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        public void Show(string exception)
        {
            gameObject.SetActive(true);
            _text.text = exception;
        }

        private void OnRestartClicked()
        {
            gameObject.SetActive(false);
            //Msg.Publish(new GameRestartRequest());
        }
    }
}