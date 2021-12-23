using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRemover : MonoBehaviour
{
    public const float time = 1;
    public bool autoShutDown = false;

    bool shutingDown = false;

    void Start()
    {
        if (autoShutDown) StartCoroutine(DelayedShutDown());
    }

    public void ShutDown(float time = time)
    {
        if (!shutingDown) StartCoroutine(DelayedShutDown(time));
    }

    IEnumerator DelayedShutDown(float time = time)
    {
        shutingDown = true;
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }
}
