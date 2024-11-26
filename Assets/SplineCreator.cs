using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SplineCreator : MonoBehaviour
{
    [Header("Spline Settings")]
    public List<Transform> controlPoints; // Points that define the spline
    public int subdivisions = 20; // Smoothness of the spline

    [Header("Debug Settings")]
    public bool showDebugLines = true; // Show lines in the editor
    public float debugPointSize = 0.2f; // Size of debug points
    public Color debugLineColor = Color.green; // Color of the spline
    public Color debugPointColor = Color.red; // Color of the control points

    private void OnDrawGizmos()
    {
        if (controlPoints == null || controlPoints.Count < 2)
            return;

        Gizmos.color = debugPointColor;
        foreach (var point in controlPoints)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, debugPointSize);
        }

        if (showDebugLines)
        {
            Gizmos.color = debugLineColor;
            Vector3 previousPoint = controlPoints[0].position;

            for (int i = 0; i < controlPoints.Count - 1; i++)
            {
                Transform p0 = i == 0 ? controlPoints[i] : controlPoints[i - 1];
                Transform p1 = controlPoints[i];
                Transform p2 = controlPoints[i + 1];
                Transform p3 = i + 2 < controlPoints.Count ? controlPoints[i + 2] : controlPoints[i + 1];

                for (int j = 0; j <= subdivisions; j++)
                {
                    float t = j / (float)subdivisions;
                    Vector3 pointOnSpline = CalculateCatmullRomPoint(t, p0.position, p1.position, p2.position, p3.position);
                    Gizmos.DrawLine(previousPoint, pointOnSpline);
                    previousPoint = pointOnSpline;
                }
            }
        }
    }

    public Vector3 CalculateCatmullRomPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            2f * p1 +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }
}
