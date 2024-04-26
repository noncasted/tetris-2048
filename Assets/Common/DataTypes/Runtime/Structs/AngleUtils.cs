using UnityEngine;

namespace Common.DataTypes.Runtime.Structs
{
    public static class AngleUtils
    {
        public static Horizontal ToHorizontal(float angle)
        {
            if (angle is > 90f and < 270f)
                return Horizontal.Left;

            return Horizontal.Right;
        }

        public static void RotateAlong(this Transform transform, Vector2 direction)
        {
            var angle = direction.ToAngle();
            var rotation = Quaternion.Euler(0f, 0f, angle);

            transform.rotation = rotation;
        }

        public static Vector2 ToDirection(this float angle)
        {
            var radians = angle * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians);
            var y = Mathf.Sin(radians);
            var direction = new Vector2(x, y);

            return direction;
        }
        
        public static Vector3 ToPlainDirection(this float angle)
        {
            var radians = angle * Mathf.Deg2Rad;
            var x = Mathf.Sin(radians);
            var z = Mathf.Cos(radians);
            var direction = new Vector3(x, 0f, z);

            return direction;
        }
    }
}