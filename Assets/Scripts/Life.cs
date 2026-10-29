// Life.cs
// Maintainer: Qingzhe-Song
// Summary: Holds current health, invokes death listeners at zero health, and destroys its GameObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [Header("Health")]
    [InspectorName("Current Health")]
    [Tooltip("Remaining health. Zero or a negative value triggers death in Update.")]
    public float amount;

    [Header("Events")]
    [Tooltip("Invoked before destruction so listeners can award points or spawn effects.")]
    public UnityEvent onDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (amount <= 0)
        {
            // Notify listeners while this object's components and transform still exist.
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }
}
