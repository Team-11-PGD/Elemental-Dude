using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SmallTornado : MonoBehaviour
{
    public Rigidbody player;
    public float normalGravityMultiplier = 1;

    [SerializeField]
    [Range(0, 360)]
    float minRotation = 5, maxRotation = 90;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float force = 1;
    [SerializeField]
    ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem.trigger.AddCollider(player.GetComponent<Collider>());
    }

    void Update()
    {
        //transform.Rotate(Vector3.up, Random.Range(minRotation, maxRotation) * (Random.Range(0f, 1f) > 0.5f ? 1 : -1));
        //transform.position += transform.up * speed * Time.deltaTime;
    }

    void OnParticleTrigger()
    {
        List<Particle> insideParticles = new List<Particle>();
        particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, insideParticles);

        if (insideParticles.Count > 0)
        {
            player.AddForce(Vector3.up * force, ForceMode.VelocityChange);
        }
    }
}
