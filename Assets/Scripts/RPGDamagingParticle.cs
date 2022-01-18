using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RPGDamagingParticle : MonoBehaviour
{
    public float damage;

    [SerializeField] float radius = 1;

    void Start()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider other in collisions)
        {
            if (other.TryGetComponent(out Health health))
            {
                health.Hit(damage);
                //SOUND: (hit sound)?
            }
        }
    }
}
