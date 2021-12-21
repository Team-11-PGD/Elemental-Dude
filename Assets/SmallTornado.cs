using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SmallTornado : MonoBehaviour
{
    public MovementScript player;
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

    [SerializeField]
    float timer, maxTimer = 2;

    private void Start()
    {
        particleSystem.trigger.AddCollider(player.GetComponent<Collider>());
    }

    void Update()
    {
        transform.Rotate(Vector3.up, Random.Range(minRotation, maxRotation) * (Random.Range(0f, 1f) > 0.5f ? 1 : -1));
        transform.position += transform.forward * speed * Time.deltaTime;
        if (timer > 0)
        {
            player.GetComponent<CharacterController>().Move(Vector3.up * force);
            timer -= Time.deltaTime;
        }
        else
        {
            player.gravityMultiplier = normalGravityMultiplier;
        }
    }

    void OnParticleTrigger()
    {
        List<Particle> enteredParticles = new List<Particle>();
        particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteredParticles);

        if (enteredParticles.Count > 0)
        {
            Debug.Log("enter");
            player.gravityMultiplier = 0;
            timer = maxTimer;
            player.ySpeed = 0;
        }
    }
}
