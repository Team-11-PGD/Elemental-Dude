using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    [SerializeField]
    private float destroyTime = 10f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Timer());
    }

    internal void SetVelocity(Vector3 forward)
    {
        rb.velocity = forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collided(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collided(collision.collider);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(destroyTime);
        Destroy(gameObject);
    }

    protected override void Hit(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            DamageHandler(health, other.gameObject.GetComponent<ElementMain>());
        }

        //SOUND: (inpact sound)?
    }

    protected override void Collided(Collider other)
    {
        Debug.Log(other.name);
        base.Collided(other);
        if (gameObject != null && !other.CompareTag("Bullet") && !other.CompareTag("IgnoreBullet")) Destroy(gameObject);
    }
}
