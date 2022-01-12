using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joshua Knaven
public class Fireball : Projectile
{
    [SerializeField]
    GameObject explosion;

    Health playerHealth;
    Collider playerModel;

    bool hit = false;

    public void SetupParticleDamage(Health playerHealth, Collider playerModel, float damage)
    {
        this.playerHealth = playerHealth;
        this.playerModel = playerModel;
        damageAmount = damage;
    }

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

        GameObject instance = Instantiate(explosion, transform.position, Quaternion.identity);

        PlayerDamagingParticle shockWave = instance.GetComponentInChildren<PlayerDamagingParticle>();
        shockWave.damage = damageAmount;
        shockWave.playerHealth = playerHealth;
        shockWave.GetComponent<ParticleSystem>().trigger.AddCollider(playerModel);

        Destroy(gameObject);
    }
}
