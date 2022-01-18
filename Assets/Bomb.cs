using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Health playerHealth;  
    [SerializeField]
    float damage = 1f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerHealth.Hit(damage);
        }
    }
}
