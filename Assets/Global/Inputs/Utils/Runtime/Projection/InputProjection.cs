using Global.Cameras.Utils.Abstract;
using Global.Debugs.Drawing.Abstract;
using Global.Inputs.Utils.Abstract;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Global.Inputs.Utils.Runtime.Projection
{
    public class InputProjection : IInputProjection
    {
        public InputProjection(ICameraUtils cameraUtils, IShapeDrawer shapeDrawer)
        {
            _cameraUtils = cameraUtils;
            _shapeDrawer = shapeDrawer;
        }

        private readonly ICameraUtils _cameraUtils;
        private readonly IShapeDrawer _shapeDrawer;

        public float GetAngleFrom(Vector3 from)
        {
            var direction = GetDirectionFrom(from);
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (angle < 0f)
                angle += 360f;
            
            return angle;
        }

        public Vector3 GetDirectionFrom(Vector3 from)
        {
            var screenPosition = Mouse.current.position.ReadValue();
            var worldPosition = _cameraUtils.ScreenToWorld(screenPosition);

            var direction = worldPosition - from;
            direction.Normalize();

            return direction;
        }

    }
}