using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleRemover : MonoBehaviour
{
    [SerializeField]
    bool autoShutDown = false;
    [SerializeField]
    float time = 0f;
    bool shutingDown;

    void Start()
    {
        if (autoShutDown) StartCoroutine(ParticleShutdown());
    }

    public void ShutDown()
    {
        if (!shutingDown) StartCoroutine(ParticleShutdown());
    }

    IEnumerator ParticleShutdown()
    {
        shutingDown = true;
        yield return new WaitForSecondsRealtime(time);
        List<ParticleSystem> particles = GetComponentsInChildren<ParticleSystem>().ToList();
        ParticleSystem parent = GetComponent<ParticleSystem>();
        particles.Add(parent);
        parent.Stop();

        yield return new WaitUntil(() =>
        {
            foreach (ParticleSystem particle in particles)
            {
                if (!particle.isStopped) return false;
            }
            return true;
        });
        Destroy(gameObject);
    }
}
