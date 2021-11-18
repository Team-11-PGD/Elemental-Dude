using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : StateMachine
{
    public Transform playerModel;
    public Health playerHealth;

    [SerializeField]
    protected Health health;
}

