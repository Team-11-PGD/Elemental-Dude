using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    [SerializeField]
    private float destroyTime = 30f;
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
        if (!other.CompareTag("Ground"))
        {
            DamageHandler(other.gameObject.GetComponent<Health>(), other.gameObject.GetComponent<ElementMain>());
        }

        //SOUND: (inpact sound)?
        Destroy(gameObject);
    }
}
