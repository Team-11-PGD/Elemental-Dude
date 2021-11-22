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
        context.GetComponent<Collider>().enabled = false;
        stateId = context.CurrentStateId;
        renderer.enabled = false;
        GameObject instance = Instantiate(fracturedModel, context.transform.position, context.transform.rotation, context.transform);
        foreach (Rigidbody rigidbody in instance.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.AddForce(Vector3.up * explosionForce);
        }
        context.enabled = false;
    }

    public override void Exit(int nextStateId)
    {
        // lock this state
        if (context.CurrentStateId != stateId)
            context.TransitionTo(context.CurrentStateId);
    }

    
}
