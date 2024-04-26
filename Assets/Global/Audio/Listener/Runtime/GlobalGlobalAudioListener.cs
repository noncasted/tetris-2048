using Global.Audio.Listener.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;

namespace Global.Audio.Listener.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GlobalGlobalAudioListener))]
    public class GlobalGlobalAudioListener :
        MonoBehaviour,
        IGlobalAudioListener,
        IScopeAwakeListener
    {
        private AudioListener _listener;

        public void OnAwake()
        {
            _listener = GetComponent<AudioListener>();
            Enable();
            
        }

        public void Enable()
        {
            _listener.enabled = true;
        }

        public void Disable()
        {
            _listener.enabled = false;
        }
    }
}