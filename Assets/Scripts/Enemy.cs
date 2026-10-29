// Enemy.cs
// Maintainer: Qingzhe-Song
// Summary: Registers with EnemiesManager on Start and unregisters when destroyed.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Start runs after scene Awake calls, when the manager establishes its instance.
        EnemiesManager.instance.AddEnemy(this);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EnemiesManager.instance.RemoveEnemy(this);
    }
}
