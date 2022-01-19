using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : FireBossState
{
    [SerializeField]
    GameObject fracturedModel;
    [SerializeField]
    Renderer renderer;
    [SerializeField]
    float explosionForce = 1f;
    [SerializeField]
    GameObject[] portals;

    int stateId;

    public override void Enter(int previousStateId)
    {
        AudioManager.instance.StopSoundFromWorld(AudioManager.instance.AmbianceSounds, "BossMusic");

        context.GetComponent<Collider>().enabled = false;
        stateId = context.CurrentStateId;
        renderer.enabled = false;
        GameObject instance = Instantiate(fracturedModel, context.transform.position, context.transform.rotation, context.transform);
        foreach (Rigidbody rigidbody in instance.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.AddForce(Vector3.up * explosionForce);
        }
        foreach (Renderer childRenderer in instance.GetComponentsInChildren<Renderer>())
        {
            childRenderer.material = renderer.material;
        }
        context.enabled = false;
        foreach (GameObject portal in portals)
        {
            portal.SetActive(true);
        }

        bossAI.animal.State_Activate(8);
    }

    public override void Exit(int nextStateId)
    {
        // lock this state
        if (context.CurrentStateId != stateId)
            context.TransitionTo((FireBossAI.StateOptions)context.CurrentStateId);
    }


}
