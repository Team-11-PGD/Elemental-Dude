using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joshua Knaven
public class BossActivator : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    GameObject rocks;
    [SerializeField]
    [TagSelector]
    string playerTag;

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (Vector3.Dot(transform.forward, other.transform.position - transform.position) >= 0)
        {
            boss.SetActive(true);
            rocks.SetActive(true);
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
