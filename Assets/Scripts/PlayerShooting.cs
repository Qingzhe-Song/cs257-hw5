// PlayerShooting.cs
// Maintainer: Qingzhe-Song
// Summary: Spawns a projectile at the weapon's shoot point and plays the muzzle effect.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Projectile")]
    [InspectorName("Projectile Prefab")]
    [Tooltip("Projectile instantiated when the Fire input calls OnFire.")]
    public GameObject prefab;
    [Tooltip("Object whose position and rotation define the projectile's spawn pose.")]
    public GameObject shootPoint;

    [Header("Weapon Effects")]
    [Tooltip("Particle system played each time the player fires.")]
    public ParticleSystem muzzleEffect;

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     GameObject clone = Instantiate(prefab);
        //     clone.transform.position = shootPoint.transform.position;
        //     clone.transform.rotation = shootPoint.transform.rotation;
        // }
    }

    public void OnFire()
    {
        // Keep this callback name so the PlayerInput Send Messages binding can find it.
        GameObject clone = Instantiate(prefab);
        clone.transform.position = shootPoint.transform.position;
        clone.transform.rotation = shootPoint.transform.rotation;
        muzzleEffect.Play();
    }
}
