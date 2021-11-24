using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DamagingParticle : MonoBehaviour
{
    public float damage;
    public Health playerHealth;

    [SerializeField]
    bool isTrigger = false;
    [SerializeField]
    ParticleSystem particleSystem;

    private void OnParticleTrigger()
    {
        if (!isTrigger) return;

        List<Particle> particles = new List<Particle>();
        particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
        if (particles.Count > 0)
            playerHealth.Hit(damage);
    }

    void OnParticleCollision(GameObject other)
    {
        if (isTrigger) return;

        if (other.CompareTag("Player"))
        {
            playerHealth.Hit(damage);
            //SOUND: (hit sound)?
        }
    }
}
