using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBulletProjectille : BulletProjectile
{
    [SerializeField] GameObject RPGExplosion;

    protected override void Collided(Collider other)
    {
        if (!other.CompareTag("IgnoreBullet"))
            Instantiate(RPGExplosion, transform.position, Quaternion.identity, null).GetComponentInChildren<RPGDamagingParticle>().damage = damageAmount;

        base.Collided(other);
    }
}
