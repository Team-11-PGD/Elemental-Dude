using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joshua Knaven
public class BossActivator : MonoBehaviour
{
    [SerializeField]
    GameObject waterBoss, fireBoss, airBoss;
    [SerializeField]
    GameObject rocks;
    [SerializeField]
    [TagSelector]
    string playerTag;

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (Vector3.Dot(transform.forward, other.transform.position - transform.position) >= 0)
        {
            switch (RoomGeneration.CurrentElements[0])
            {
                case ElementMain.ElementType.Water:
                    waterBoss?.SetActive(true);
                    break;
                case ElementMain.ElementType.Fire:
                    fireBoss?.SetActive(true);
                    break;
                case ElementMain.ElementType.Air:
                    airBoss?.SetActive(true);
                    break;
            }
            rocks.SetActive(true);
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
