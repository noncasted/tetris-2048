using System.Collections.Generic;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Connections.Runtime
{
    public class SplineGenerator
    {
        public static List<Vector2> GenerateSpline(List<Vector2> points, int stepsPerCurve = 3, float tension = 1)
        {
            var result = new List<Vector2>();

            for (var i = 0; i < points.Count - 1; i++)
            {
                var prev = i == 0 ? points[i] : points[i - 1];
                var currStart = points[i];
                var currEnd = points[i + 1];
                var next = i == points.Count - 2 ? points[i + 1] : points[i + 2];

                for (var step = 0; step <= stepsPerCurve; step++)
                {
                    var t = (float)step / stepsPerCurve;
                    var tSquared = t * t;
                    var tCubed = tSquared * t;

                    var interpolatedPoint =
                        (-.5f * tension * tCubed + tension * tSquared - .5f * tension * t) * prev +
                        (1 + .5f * tSquared * (tension - 6) + .5f * tCubed * (4 - tension)) * currStart +
                        (.5f * tCubed * (tension - 4) + .5f * tension * t - (tension - 3) * tSquared) * currEnd +
                        (-.5f * tension * tSquared + .5f * tension * tCubed) * next;

                    result.Add(interpolatedPoint);
                }
            }

            return result;
        }
    }
}