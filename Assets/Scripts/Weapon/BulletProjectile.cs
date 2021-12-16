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
    }

    protected override void Hit(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            DamageHandler(other.gameObject.GetComponent<Health>(), other.gameObject.GetComponent<ElementMain>());
        }

        //SOUND: (inpact sound)?
        Destroy(gameObject);
    }
}
