using Global.Cameras.CurrentProvider.Abstract;
using Global.Cameras.Utils.Abstract;
using Global.Debugs.Drawing.Abstract;
using UnityEngine;

namespace Global.Cameras.Utils.Runtime
{
    public class CameraUtils : ICameraUtils
    {
        public CameraUtils(
            IShapeDrawer shapeDrawer,
            ICurrentCameraProvider currentCameraProvider)
        {
            _shapeDrawer = shapeDrawer;
            _currentCameraProvider = currentCameraProvider;
        }

        private readonly IShapeDrawer _shapeDrawer;
        private readonly ICurrentCameraProvider _currentCameraProvider;

        public Vector3 ScreenToWorld(Vector3 screen)
        {
            if (_currentCameraProvider.Current == null)
                return Vector3.zero;

            var ray = _currentCameraProvider.Current.ScreenPointToRay(screen);
            Physics.Raycast(ray, out var info, float.PositiveInfinity, LayerMask.GetMask("Collision", "Default"));
            var world = info.point;
            _shapeDrawer.DrawCircle(world, 0.2f);

            return world;
        }
    }
}