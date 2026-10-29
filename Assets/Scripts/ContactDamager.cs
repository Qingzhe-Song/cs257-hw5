// ContactDamager.cs
// Maintainer: Qingzhe-Song
// Summary: Destroys itself on trigger contact and damages a Life component on the other collider.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{
    [Header("Contact Damage")]
    [Tooltip("Health subtracted if the other collider has a Life component.")]
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Destroy is deferred, allowing this contact to apply damage before cleanup.
        Destroy(gameObject);
        Life life = other.GetComponent<Life>();

        if (life != null)
        {
            life.amount -= damage;
        }
    }
}
