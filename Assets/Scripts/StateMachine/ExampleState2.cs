using System.Collections;
using UnityEngine;


public class ExampleState2 : State
{
    public override void Enter(int previousStateId)
    {
        StartCoroutine(Timer(2));
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        context.transform.position += context.transform.forward * 0.01f;
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // See ExampleState1 for explanation
        context.TransitionTo((int)AIExample.StateOptions.EnemyDefending);
    }
}


