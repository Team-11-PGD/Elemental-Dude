using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject rocks;
    float bossActivationRange = 20;

    private void OnTriggerExit(Collider other)
    {
        if (Vector3.Distance(other.transform.position, transform.position) <= bossActivationRange)
        {
            boss.SetActive(true);
            rocks.SetActive(true);
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
