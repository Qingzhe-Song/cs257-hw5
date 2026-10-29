// ScoreManager.cs
// Maintainer: Qingzhe-Song
// Summary: Exposes the scene's shared score through a singleton instance.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [InspectorName("Current Score")]
    [Tooltip("Running score increased by ScoreOnDeath components.")]
    public int amount;
    public static ScoreManager instance;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // Keep the first manager as the shared score source.
            Debug.LogError("Duplicated ScoreManager, ignoring this one", gameObject);
        }
    }
}
