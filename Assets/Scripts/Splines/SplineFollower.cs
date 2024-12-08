using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    [Header("Spline Settings")]
    public SplineCreator spline; // Reference to the SmoothSpline script
    public float speed = 1f; // Speed of the movement
    public bool loop = false; // Should the movement loop?
    public bool isPlaying = false;

    private float t = 0f; // Parameter to track position on the spline
    private int currentSegment = 0; // Current segment of the spline
    private bool isAtEnd = false; // Tracks if the object reached the end

    private void Update()
    {
        if (spline == null || spline.controlPoints.Count < 1 || isAtEnd || !isPlaying)
            return;

        MoveAlongSpline();
    }

    private void MoveAlongSpline()
    {
        // Check if the object has reached the last segment
        if (currentSegment >= spline.controlPoints.Count - 1)
        {
            if (loop)
            {
                currentSegment = 0;
                t = 0f;
            }
            else
            {
                // Stop the follower at the final control point
                transform.position = spline.controlPoints[spline.controlPoints.Count - 1].position;
                isAtEnd = true;
                return;
            }
        }

        // Get the points for the current segment
        Transform p0 = currentSegment == 0 ? spline.controlPoints[currentSegment] : spline.controlPoints[currentSegment - 1];
        Transform p1 = spline.controlPoints[currentSegment];
        Transform p2 = currentSegment + 1 < spline.controlPoints.Count ? spline.controlPoints[currentSegment + 1] : spline.controlPoints[currentSegment];
        Transform p3 = currentSegment + 2 < spline.controlPoints.Count ? spline.controlPoints[currentSegment + 2] : p2;

        // Move along the spline
        t += speed * Time.deltaTime / spline.subdivisions;

        if (t >= 1f)
        {
            t -= 1f;
            currentSegment++;
        }

        // Calculate the position and forward direction
        Vector3 position = spline.CalculateCatmullRomPoint(t, p0.position, p1.position, p2.position, p3.position);
        Vector3 forward = CalculateSplineTangent(t, p0.position, p1.position, p2.position, p3.position).normalized;

        // Update the position and rotation
        transform.position = position;
        if (forward != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(forward);
    }


    private Vector3 CalculateSplineTangent(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float t2 = t * t;

        // Derivative of Catmull-Rom spline formula
        return 0.5f * (
            (-p0 + p2) +
            2f * t * (2f * p0 - 5f * p1 + 4f * p2 - p3) +
            3f * t2 * (-p0 + 3f * p1 - 3f * p2 + p3)
        );
    }
}
