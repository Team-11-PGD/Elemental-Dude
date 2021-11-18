using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingParticle : MonoBehaviour
{
    public float damage;
    public Health playerHealth;

    void OnParticleTrigger()
    {
        //SOUND: (hit sound)?
        playerHealth.Hit(damage);
    }
}
