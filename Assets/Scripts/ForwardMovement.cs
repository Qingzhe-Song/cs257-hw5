// ForwardMovement.cs
// Maintainer: Qingzhe-Song
// Summary: Moves its transform along its local forward axis every frame.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [Header("Forward Motion")]
    [Tooltip("Movement speed in world units per second along the local positive Z axis.")]
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
