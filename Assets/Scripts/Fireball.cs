using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    [SerializeField]
    GameObject explosion;

    Health playerHealth;
    Collider playerModel;

    bool hit = false;

    public void SetupParticleDamage(Health playerHealth, Collider playerModel)
    {
        this.playerHealth = playerHealth;
        this.playerModel = playerModel;
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

        GameObject instance = Instantiate(explosion, transform.position, Quaternion.identity, null);

        DamagingParticle shockWave = instance.GetComponentInChildren<DamagingParticle>();
        shockWave.damage = damageAmount;
        shockWave.playerHealth = playerHealth;
        shockWave.GetComponent<ParticleSystem>().trigger.AddCollider(playerModel);

        Destroy(gameObject);
    }
}
