using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
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
        GameObject clone = Instantiate(prefab);
        clone.transform.position = shootPoint.transform.position;
        clone.transform.rotation = shootPoint.transform.rotation;
        muzzleEffect.Play();
    }
}
