using Global.Cameras.Persistent.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;

namespace Global.Cameras.Persistent.Runtime
{
    [DisallowMultipleComponent]
    public class GlobalCamera : MonoBehaviour, IGlobalCamera, IScopeAwakeListener
    {
        public Camera Camera { get; private set; }

        public void OnAwake()
        {
            Camera = GetComponent<Camera>();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}