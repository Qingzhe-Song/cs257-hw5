using UnityEngine;

public class ExplosionOnDeath : MonoBehaviour
{
    public GameObject particlePrefab;

    private void Awake()
    {
        var life = GetComponent<Life>();
        life.onDeath.AddListener(SpawnExplosion);
    }

    private void SpawnExplosion()
    {
        Instantiate(
            particlePrefab,
            transform.position,
            transform.rotation
        );
    }
}