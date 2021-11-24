using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    internal void SetVelocity(Vector3 forward)
    {
        rb.velocity = forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collided(other);
        //SOUND: (inpact sound)?
        Destroy(gameObject);
    }
}
