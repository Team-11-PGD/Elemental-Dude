     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveScript : Projectile
{
    [SerializeField]
    protected Transform player, boss;

    protected Rigidbody rigidBody;

    [SerializeField]
    protected float waveSpeed, waveForce, waveStunDuration;

    protected bool canDamage;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce((player.position - boss.position).normalized * waveSpeed);
        canDamage = true;
    }

    protected virtual void FixedUpdate()
    {
        gameObject.transform.localScale += new Vector3(0.05f, 0.1f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.gameObject.tag == "Player")
        {
            Collided(other);
            canDamage = false;
        }
        if(other.gameObject.tag == "OutOfBounds") Destroy(gameObject);
    }

    protected override void Hit(Collider other)
    {
        if(other.tag == "Player")
        {
            DamageHandler(other.gameObject.GetComponentInParent<Health>(), other.gameObject.GetComponentInParent<ElementMain>());
            MovementScript movement = other.gameObject.GetComponent<MovementScript>();
            //stuns player and moves him back
            movement.stunDuration = waveStunDuration;
            movement.velocity = rigidBody.velocity * waveForce;
        }
    }

    public void GiveTarget(Transform boss, Transform player)
    {
        this.boss = boss;
        this.player = player;
    }
}
