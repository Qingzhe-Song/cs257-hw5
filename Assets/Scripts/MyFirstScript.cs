// MyFirstScript.cs
// Maintainer: Qingzhe-Song
// Summary: Demonstrates Unity lifecycle messages by logging Start, a field value, and destruction.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirstScript : MonoBehaviour
{
    [Header("Logging Example")]
    [Tooltip("Example value printed each frame; this demonstration does not move the object.")]
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        print("Test Start");
    }

    // Update is called once per frame
    void Update()
    {
        print(speed);
    }

    // Happens when the game stops
    void OnDestroy()
    {
        print("Test Destroyed");
    }
}
