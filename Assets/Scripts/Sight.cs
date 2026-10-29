// Sight.cs
// Maintainer: Qingzhe-Song
// Summary: Finds the first collider within range and viewing angle that has an unobstructed line of sight.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [Header("Detection Range")]
    [InspectorName("Detection Distance")]
    [Tooltip("Radius in world units used to search for candidate colliders.")]
    public float distance;
    [InspectorName("View Half Angle (Degrees)")]
    [Tooltip("Maximum angle from forward on either side; the full viewing cone is twice this value.")]
    public float angle;

    [Header("Detection Layers")]
    [Tooltip("Layers containing colliders that this sensor can detect.")]
    public LayerMask objectsLayers;
    [Tooltip("Layers that block the line from the sensor to a candidate collider's bounds center.")]
    public LayerMask obstaclesLayers;

    [Header("Detection Result")]
    [Tooltip("First visible candidate this frame, or null when no candidate passes the checks.")]
    public Collider detectedObject;
    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position, distance, objectsLayers
        );

        // Clear the previous result so enemies can react when the player leaves sight.
        detectedObject = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];
            Vector3 directionToController = Vector3.Normalize(collider.bounds.center - transform.position);

            float angleToCollider = Vector3.Angle(transform.forward, directionToController);

            if (angleToCollider < angle)
            {
                if (!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstaclesLayers))
                {
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    detectedObject = collider;
                    // Use the first visible overlap result; candidates are not sorted by distance.
                    break;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection * distance);

        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * distance);
    }
}
