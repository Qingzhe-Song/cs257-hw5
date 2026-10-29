// Autodestroy.cs
// Maintainer: Qingzhe-Song
// Summary: Destroys its GameObject after a configured lifetime.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Lifetime")]
    [InspectorName("Lifetime (Seconds)")]
    [Tooltip("Time after Start before this GameObject is destroyed.")]
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
