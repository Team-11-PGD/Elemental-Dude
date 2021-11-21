using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : State
{
    [SerializeField]
    GameObject fracturedModel;
    [SerializeField]
    Renderer renderer;
    [SerializeField]
    float explosionForce = 1f;
    
    int stateId;

    public override void Enter(int previousStateId)
    {
        stateId = context.CurrentStateId;
        renderer.enabled = false;
        GameObject instance = Instantiate(fracturedModel, context.transform.position, Quaternion.identity, context.transform);
        foreach (Rigidbody rigidbody in instance.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.AddForce(Vector3.up * explosionForce);
        }
    }

    public override void Exit(int nextStateId)
    {
        // lock this state
        if (context.CurrentStateId != stateId)
            context.TransitionTo(context.CurrentStateId);
    }

    
}
