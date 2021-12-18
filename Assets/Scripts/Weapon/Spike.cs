using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joshua Knaven
public class Spike : MonoBehaviour
{
    public Health playerHealth;
    public bool isGroundSpike = true;

    [SerializeField]
    float timeActive = 1f;
    [SerializeField]
    float damage = 0.1f;
    [SerializeField]
    float upDistance = 1;
    [SerializeField]
    float spikeEasing = 0.1f;
    [SerializeField]
    float spikePrecision = 0.01f;

    Vector3 upPosition;
    float timer = 0;

    void Start()
    {
        upPosition = transform.position + Vector3.up * (isGroundSpike ? upDistance : -upDistance);
        StartCoroutine(MoveUp());
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= timeActive)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!enabled) return;

        if (collider.CompareTag("Player"))
        {
            playerHealth.Hit(damage);
        }
    }

    IEnumerator MoveUp()
    {
        while (Vector3.Distance(transform.position, upPosition) > spikePrecision)
        {
            transform.position = Vector3.Lerp(transform.position, upPosition, spikeEasing);
            yield return null;
        }
    }
}
