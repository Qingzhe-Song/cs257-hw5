// WavesGameMode.cs
// Maintainer: Qingzhe-Song
// Summary: Loads the win scene after all waves and enemies clear, or the lose scene on player/base death.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [Header("Defended Objects")]
    [Tooltip("Player health component whose death triggers the LoseScreen scene.")]
    [SerializeField] Life playerLife;
    [Tooltip("Base health component whose death also triggers the LoseScreen scene.")]
    [SerializeField] Life playerBaseLife;
    // Start is called before the first frame update
    void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerOrBaseDied);
        playerBaseLife.onDeath.AddListener(OnPlayerOrBaseDied);
        EnemiesManager.instance.onChanged.AddListener(CheckWinCondition);
        WaveManager.instance.onChanged.AddListener(CheckWinCondition);
    }

    // Update is called once per frame
    void CheckWinCondition()
    {
        // Both lists must be empty: a quiet gap between scheduled spawns is not a win.
        if (EnemiesManager.instance.enemies.Count <= 0 && WaveManager.instance.waves.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }

    void OnPlayerOrBaseDied()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    void OnDestroy()
    {
        // Release runtime event subscriptions when the game mode leaves the scene.
        playerLife.onDeath.RemoveListener(OnPlayerOrBaseDied);
        playerBaseLife.onDeath.RemoveListener(OnPlayerOrBaseDied);

        EnemiesManager.instance.onChanged.RemoveListener(CheckWinCondition);
        WaveManager.instance.onChanged.RemoveListener(CheckWinCondition);
    }
}
