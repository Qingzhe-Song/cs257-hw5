// ScoreOnDeath.cs
// Maintainer: Qingzhe-Song
// Summary: Awards points through ScoreManager when the attached Life component signals death.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    [Header("Score Reward")]
    [InspectorName("Points On Death")]
    [Tooltip("Points added to the shared score when this object's Life component signals death.")]
    public int amount;
    
    private void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(GivePoints);
    }
    void GivePoints()
    {
        ScoreManager.instance.amount += amount;
    }

    void OnDestroy()
    {
        // Pair the runtime subscription with cleanup when this component is destroyed.
        var life = GetComponent<Life>();
        life.onDeath.RemoveListener(GivePoints);
    }
}
