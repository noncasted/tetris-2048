using UnityEngine;

namespace Global.Cameras.CurrentProvider.Abstract
{
    public interface ICurrentCameraProvider
    {
        Camera Current { get; }

        void SetCamera(Camera current);
    }
}