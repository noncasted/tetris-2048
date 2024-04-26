using UnityEngine;

namespace Global.Cameras.Utils.Abstract
{
    public interface ICameraUtils
    {
        Vector3 ScreenToWorld(Vector3 screen);
    }
}