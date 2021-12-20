     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveScript : Projectile
{
    [SerializeField]
    public Transform player;
    [SerializeField]
    public Transform boss;

    [SerializeField]
    float waveSpeed;

    [SerializeField]
    float waveForce;
    [SerializeField]
    float waveStunDuration;

    bool canDamage;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce((player.position - boss.position).normalized * waveSpeed);
        canDamage = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.gameObject.tag == "Player")
        {
            Collided(other);
            canDamage = false;
        }
        if(other.gameObject.tag == "OutOfBounds")
        {
            Destroy(gameObject);
        }

    }

    protected override void Hit(Collider other)
    {
        if(other.tag == "Player")
        {
            DamageHandler(other.gameObject.GetComponentInParent<Health>(), other.gameObject.GetComponentInParent<ElementMain>());
            other.gameObject.GetComponent<MovementScript>().stunDuration = waveStunDuration;
            other.gameObject.GetComponent<MovementScript>().stunned = true;
            other.gameObject.GetComponent<MovementScript>().velocity = Vector3.forward * waveForce;
        }
    }

    public void GiveTarget(Transform boss, Transform player)
    {
        this.boss = boss;
        this.player = player;        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
