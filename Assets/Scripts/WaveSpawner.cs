// WaveSpawner.cs
// Maintainer: Qingzhe-Song
// Summary: Registers a timed enemy wave, spawns its prefab repeatedly, and unregisters when the wave ends.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawned Enemy")]
    [InspectorName("Enemy Prefab")]
    [Tooltip("Enemy prefab spawned at this spawner's position and rotation.")]
    public GameObject prefab;

    [Header("Wave Timing")]
    [InspectorName("Start Delay (Seconds)")]
    [Tooltip("Delay from Start until the first spawn.")]
    public float startTime;
    [InspectorName("Wave End Delay (Seconds)")]
    [Tooltip("Delay from Start until this wave ends, measured independently of the first spawn.")]
    public float endTime;
    [InspectorName("Spawn Interval (Seconds)")]
    [Tooltip("Seconds between spawns. Use a positive value for repeated spawning.")]
    public float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        // Register before the first spawn so a delayed wave still counts toward the win condition.
        WaveManager.instance.AddWave(this);
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("EndSpawner", endTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    void EndSpawner()
    {
        // Only future spawns end here; existing enemies remain tracked by EnemiesManager.
        WaveManager.instance.RemoveWave(this);
        CancelInvoke();
    }
}
