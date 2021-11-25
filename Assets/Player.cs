using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Health health;

    void OnEnable()
    {
        health.Hitted += Hitted;
        health.Died += Died;
        health.Healed += Healed;
    }

    void OnDisable()
    {
        health.Hitted -= Hitted;
        health.Died -= Died;
        health.Healed -= Healed;
    }

    void Hitted()
    {
        //SOUND: (Player hit
    }

    void Died()
    {
        //SOUND: (Player death)
    }

    void Healed()
    {
        //SOUND: (Player heal)
    }
}