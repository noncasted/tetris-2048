using UnityEngine;

namespace Global.Cameras.Persistent.Abstract
{
    public interface IGlobalCamera
    {
        Camera Camera { get; }
        void Enable();
        void Disable();
    }
}