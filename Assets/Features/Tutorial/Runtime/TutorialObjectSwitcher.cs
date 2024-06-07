using Common.Components.Runtime.ObjectLifetime;
using Global.System.MessageBrokers.Abstract;
using UnityEngine;

namespace Features.Tutorial.Runtime
{
    [DisallowMultipleComponent]
    public class TutorialObjectSwitcher : MonoBehaviour
    {
        [SerializeField] private TutorialSwitchKey _key;
        [SerializeField] private GameObject _target;

        private void OnEnable()
        {
            var lifetime = this.GetObjectLifetime();

            Msg.Listen<TutorialEnableEvent>(lifetime, Enable);
            Msg.Listen<TutorialDisableEvent>(lifetime, Disable);
        }

        private void Enable(TutorialEnableEvent data)
        {
            if (data.Key != _key)
                return;

            _target.SetActive(true);
        }

        private void Disable(TutorialDisableEvent data)
        {
            if (data.Key != _key)
                return;

            _target.SetActive(false);
        }
    }
}