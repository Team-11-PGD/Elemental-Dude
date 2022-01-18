using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Projectile
{
    Health playerHealth;
    Collider playerModel;

    bool hit = false;    

    void OnCollisionEnter(Collision collision)
    {
        Collided(collision.collider);
    }

    protected override void Hit(Collider other)
    {
        // Only hit once
        if (hit) return;
        hit = true;

        if (other.CompareTag("Player"))
        {
            DamageHandler(other.gameObject.GetComponentInParent<Health>(), other.gameObject.GetComponentInParent<ElementMain>());
        }              
    }
}
