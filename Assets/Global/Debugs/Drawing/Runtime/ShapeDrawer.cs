using System.Collections.Generic;
using Drawing;
using Global.Debugs.Drawing.Abstract;
using Unity.Mathematics;
using UnityEngine;

namespace Global.Debugs.Drawing.Runtime
{
    public class ShapeDrawer : IShapeDrawer
    {
        private const float _defaultWidth = 10f;
        private readonly Color _defaultColor = Color.white;

        public void DrawSpline(List<Vector3> points, Color color, float width)
        {
            using (Draw.ingame.WithLineWidth(width))
            {
                Draw.ingame.CatmullRom(points, color);
            }
        }

        public void DrawCircle(Vector3 position, float radius)
        {
            DrawCircle(position, radius, _defaultWidth, _defaultColor);
        }

        public void DrawCircle(float duration, Vector3 position, float radius)
        {
            DrawCircle(duration, position, radius, _defaultWidth, _defaultColor);
        }

        public void DrawCircle(Vector3 position, float radius, float width)
        {
            DrawCircle(position, radius, width, _defaultColor);
        }

        public void DrawCircle(float duration, Vector3 position, float radius, float width)
        {
            DrawCircle(duration, position, radius, width, _defaultColor);
        }

        public void DrawCircle(Vector3 position, float radius, Color color)
        {
            DrawCircle(position, radius, _defaultWidth, color);
        }

        public void DrawCircle(float duration, Vector3 position, float radius, Color color)
        {
            DrawCircle(duration, position, radius, _defaultWidth, color);
        }

        public void DrawCircle(Vector3 position, float radius, float width, Color color)
        {
            using (Draw.ingame.WithLineWidth(width))
            {
                var resultPosition = new float3(position.x, position.y, position.z);

                Draw.ingame.WireSphere(resultPosition, radius, color);
            }
        }

        public void DrawCircle(float duration, Vector3 position, float radius, float width, Color color)
        {
            using (Draw.ingame.WithDuration(duration))
            {
                using (Draw.ingame.WithLineWidth(width))
                {
                    var resultPosition = new float3(position.x, position.y, 0f);

                    Draw.ingame.WireSphere(resultPosition, radius, color);
                }
            }
        }

        public void DrawLine(Vector3 start, Vector3 end)
        {
            DrawLine(start, end, _defaultWidth, _defaultColor);
        }

        public void DrawLine(float duration, Vector3 start, Vector3 end)
        {
            DrawLine(duration, start, end, _defaultWidth, _defaultColor);
        }

        public void DrawLine(Vector3 start, Vector3 end, float width)
        {
            DrawLine(start, end, width, _defaultColor);
        }

        public void DrawLine(float duration, Vector3 start, Vector3 end, float width)
        {
            DrawLine(duration, start, end, width, _defaultColor);
        }

        public void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            DrawLine(start, end, _defaultWidth, color);
        }

        public void DrawLine(float duration, Vector3 start, Vector3 end, Color color)
        {
            DrawLine(duration, start, end, _defaultWidth, color);
        }

        public void DrawLine(Vector3 start, Vector3 end, float width, Color color)
        {
            using (Draw.WithLineWidth(width))
            {
                Draw.Line(start, end, color);
            }
        }

        public void DrawLine(float duration, Vector3 start, Vector3 end, float width, Color color)
        {
            using (Draw.ingame.WithDuration(duration))
            {
                using (Draw.ingame.WithLineWidth(width))
                {
                    Draw.ingame.Line(start, end, color);
                }
            }
        }

        public void DrawRect(Vector3 center, Vector3 size, float width, float angle, Color color)
        {
            using (Draw.ingame.WithLineWidth(width))
            {
                var convertedCenter = new float3(center.x, center.y, 0f);
                var convertedSize = new float2(center.x, center.y);
                var rotation = Quaternion.Euler(0f, 0f, angle);

                Draw.ingame.WireRectangle(convertedCenter, rotation, convertedSize, color);
            }
        }
    }
}