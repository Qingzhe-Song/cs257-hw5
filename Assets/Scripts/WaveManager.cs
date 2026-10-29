// WaveManager.cs
// Maintainer: Qingzhe-Song
// Summary: Tracks registered wave spawners and notifies listeners when the wave list changes.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    [Header("Wave Tracking")]
    [Tooltip("Registered spawners that have not yet ended their waves.")]
    public List<WaveSpawner> waves;

    [Header("Events")]
    [Tooltip("Invoked after AddWave or RemoveWave updates the registered wave list.")]
    public UnityEvent onChanged;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // Keep the first manager as the shared source of active waves.
            Debug.LogError("Dupe detected, ignoring this", gameObject);
        }
    }

    public void AddWave(WaveSpawner other)
    {
        waves.Add(other);
        onChanged.Invoke();
    }

    public void RemoveWave(WaveSpawner other)
    {
        waves.Remove(other);
        onChanged.Invoke();
    }
}
